using System;
using ETGMod;
using System.IO;
using Eluant;
using System.Reflection;
using UnityEngine;
using Logger = ETGMod.Logger;

namespace Stock {
    public class Stock : MonoBehaviour {
        public const KeyCode RELOAD_KEY = KeyCode.F5;
        public const KeyCode RELOAD_MODIFIER = KeyCode.LeftControl;
        public static Logger Logger;
        private static string _DataPath;
        private static string _GamePath;
        private static string _StockPath;
        private static string _StockMainLuaPath;
        public static LuaRuntime LuaState;
        private static GameObject _GameObject;

        public static LuaFunction Unload;
        public static LuaFunction Ready;

        public static string DataPath {
            get {
                return _DataPath;
            }
            set {
                _DataPath = value;
                _GamePath = Directory.GetParent(_DataPath).FullName;
                _StockPath = Path.Combine(_GamePath, "Stock");
                _StockMainLuaPath = Path.Combine(_StockPath, "main.lua");
            }
        }

        public static string GamePath { get { return _GamePath; } }
        public static string StockPath { get { return _StockPath; } }
        public static string StockMainLuaPath { get { return _StockMainLuaPath; } }

        public static void InitializeStock() {
            if (!Directory.Exists(StockPath)) Directory.CreateDirectory(StockPath);

            InitializeLua();
            InitializeGameObject();
        }

        private static void SafeExecLua(LuaFunction func, string what) {
            try {
                func.Call();
            } catch (LuaException e) {
                Logger.Error($"Failed {what}: [{e.GetType()}]");
                Logger.ErrorIndent(e.Message);
                foreach (var line in e.TracebackArray) {
                    Logger.ErrorIndent(line);
                }
            } catch (Exception e) {
                Logger.Error($"Failed {what}: [{e.GetType()}]");
                Logger.ErrorIndent(e.Message);
                foreach (var line in e.StackTrace.Split('\n')) {
                    Logger.ErrorIndent(line.Replace("  ", ""));
                }
            }
        }

        public static void InitializeLua() {
            if (!File.Exists(StockMainLuaPath)) {
                Logger.Warn("No main.lua - bailing out.");
                return;
            }

            try {
                if (LuaState != null) DeinitializeLua();
                LuaState = new LuaRuntime();
                Logger.Info("Running Lua");
                LuaState.MonoStackTraceWorkaround = true;
                LuaState.InitializeClrPackage();
                using (var package = LuaState.Globals["package"] as LuaTable) {
                    package["path"] = $"{Path.Combine(StockPath, "?.lua")};{Path.Combine(StockPath, "?/init.lua")}";
                    package["cpath"] = "";
                }
                using (var stock_table = LuaState.CreateTable()) {
                    stock_table["_VERSION"] = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                    LuaState.Globals["stock"] = stock_table;
                }
                LuaState.DoFile(StockMainLuaPath);

                using (var stock = LuaState.Globals["stock"]) {
                    var stock_table = stock as LuaTable;
                    if (stock_table != null) {
                        var unload = stock_table["unload"];
                        if (!(unload is LuaFunction)) {
                            unload.Dispose();
                            unload = null;
                        }
                        var ready = stock_table["ready"];
                        if (!(ready is LuaFunction)) {
                            ready.Dispose();
                            ready = null;
                        }

                        if (unload != null) Unload = unload as LuaFunction;
                        if (ready != null) Ready = ready as LuaFunction;
                    }
                }
            } catch (LuaException e) {
                Logger.Error($"Failed initializing Lua: [{e.GetType()}]");
                Logger.ErrorIndent(e.Message);
                foreach (var line in e.TracebackArray) {
                    Logger.ErrorIndent(line);
                }
            } catch (Exception e) {
                Logger.Error($"Failed initializing Lua: [{e.GetType()}]");
                Logger.ErrorIndent(e.Message);
                foreach (var line in e.StackTrace.Split('\n')) {
                    Logger.ErrorIndent(line.Replace("  ", ""));
                }
            }

            if (_GameObject != null) {
                if (Ready != null) SafeExecLua(Ready, "running stock.ready");
            }
        }

        public static void DeinitializeLua() {
            if (Unload != null) SafeExecLua(Unload, "running stock.unload");
            LuaState.Dispose();
            LuaState = null;
            Unload = null;
            Ready = null;
        }

        public static void InitializeGameObject() {
            _GameObject = new GameObject("Stock: A Lua Injection Engine");
            _GameObject.AddComponent<Stock>();
            DontDestroyOnLoad(_GameObject);
        }

        // MonoBehaviour

        public void Awake() {
            Logger.Debug("Stock GameObject Awake");
            if (Ready != null) SafeExecLua(Ready, "running stock.ready");
        }

        public void Update() {
            if (Input.GetKeyDown(RELOAD_KEY) && Input.GetKey(RELOAD_MODIFIER)) {
                Logger.Info("Reloading Lua");
                InitializeLua();
            }
        }
    }
}
