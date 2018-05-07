#pragma warning disable CS0626
using System;
using MonoMod;
using ETGMod;
using UnityEngine;
using Object = UnityEngine.Object;
using Logger = ETGMod.Logger;

namespace Stock.Patches {
    [MonoModPatch("global::UnityEngine.Object")]
    class ObjectEntryPointPatch : Object {
        public static extern void orig_cctor_Object();

        [MonoModConstructor]
        [MonoModOriginalName("orig_cctor_Object")]
        public static void cctor_Object() {
            var logger = new Logger("Stock");
            Stock.Logger = logger;
            Stock.DataPath = Application.dataPath;

            logger.Info("Started.");
            logger.Info("Paths:");
            logger.InfoIndent($"Data path: '{Stock.DataPath}'");
            logger.InfoIndent($"Game path: '{Stock.GamePath}'");
            logger.InfoIndent($"Stock path: '{Stock.StockPath}'");
            logger.InfoIndent($"Main Lua path: '{Stock.StockMainLuaPath}'");

            Stock.InitializeStock();
        }
    }
}