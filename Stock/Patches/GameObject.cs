#pragma warning disable CS0626
using System;
using Stock;
using MonoMod;
using Eluant;

namespace Stock.Patches {
    [MonoModPatch("global::UnityEngine.GameObject")]
    public class GameObjectPatch {
        [MonoModIgnore]
        public extern T AddComponent<T>() where T : UnityEngine.Component;

        public LuaCustomClrObject AddLuaMonoBehaviour() {
            return new LuaCustomClrObject(AddComponent<LuaMonoBehaviour>());
        }

        public LuaCustomClrObject AddLuaBasicMonoBehaviour() {
            return new LuaCustomClrObject(AddComponent<LuaBasicMonoBehaviour>());
        }
    }
}
