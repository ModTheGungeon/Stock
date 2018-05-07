using System;
using UnityEngine;
using Eluant;
using Eluant.ObjectBinding;

namespace Stock {
    public class LuaBasicMonoBehaviour : MonoBehaviour, IDisposable, ILuaTableBinding {
        public void Dispose() {
            EventAwake.Dispose();
            EventFixedUpdate.Dispose();
            EventLateUpdate.Dispose();
            EventOnDestroy.Dispose();
            EventOnDisable.Dispose();
            EventOnEnable.Dispose();
            EventOnGUI.Dispose();
            EventStart.Dispose();
            EventUpdate.Dispose();
        }

        public LuaFunction EventAwake;
        public LuaFunction EventFixedUpdate;
        public LuaFunction EventLateUpdate;
        public LuaFunction EventOnDestroy;
        public LuaFunction EventOnDisable;
        public LuaFunction EventOnEnable;
        public LuaFunction EventOnGUI;
        public LuaFunction EventStart;
        public LuaFunction EventUpdate;

        public LuaValue this[LuaRuntime runtime, LuaValue key] {
            get {
                if (!(key is LuaString)) throw new Exception("Invalid key type - must be a string");

                var key_str = key as LuaString;

                if (key_str == "Awake") return EventAwake;
                else if (key_str == "FixedUpdate") return EventFixedUpdate;
                else if (key_str == "LateUpdate") return EventLateUpdate;
                else if (key_str == "OnDestroy") return EventOnDestroy;
                else if (key_str == "OnDisable") return EventOnDisable;
                else if (key_str == "OnEnable") return EventOnEnable;
                else if (key_str == "OnGUI") return EventOnGUI;
                else if (key_str == "Start") return EventStart;
                else if (key_str == "Update") return EventUpdate;

                return LuaNil.Instance;
            }
            set {
                if (!(key is LuaString)) throw new Exception("Invalid key type - must be a string");
                if (!(value is LuaFunction)) {
                    throw new Exception("Invalid value type - must be a function");
                }

                var key_str = key as LuaString;
                var value_func = value as LuaFunction;
                value_func.DisposeAfterManagedCall = false;

                if (key_str == "Awake") { EventAwake = value_func; return; }
                else if (key_str == "FixedUpdate") { EventFixedUpdate = value_func; return; }
                else if (key_str == "LateUpdate") { EventLateUpdate = value_func; return; }
                else if (key_str == "OnDestroy") { EventOnDestroy = value_func; return; }
                else if (key_str == "OnDisable") { EventOnDisable = value_func; return; }
                else if (key_str == "OnEnable") { EventOnEnable = value_func; return; }
                else if (key_str == "OnGUI") { EventOnGUI = value_func; return; }
                else if (key_str == "Start") { EventStart = value_func; return; }
                else if (key_str == "Update") { EventUpdate = value_func; return; }

                throw new Exception($"Invalid event name: '{key_str}'");
            }
        }

        public void Awake() {
            if (EventAwake != null) EventAwake.Call();
        }
        public void FixedUpdate() {
            if (EventFixedUpdate != null) EventFixedUpdate.Call();
        }
        public void LateUpdate() {
            if (EventLateUpdate != null) EventLateUpdate.Call();
        }
        public void OnDestroy() {
            if (EventOnDestroy != null) EventOnDestroy.Call();
        }
        public void OnDisable() {
            if (EventOnDisable != null) EventOnDisable.Call();
        }
        public void OnEnable() {
            if (EventOnEnable != null) EventOnEnable.Call();
        }
        public void OnGUI() {
            if (EventOnGUI != null) EventOnGUI.Call();
        }
        public void Start() {
            if (EventStart != null) EventStart.Call();
        }
        public void Update() {
            if (EventUpdate != null) EventUpdate.Call();
        }
    }
}
