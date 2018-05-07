using System;
using System.Reflection;
using System.Collections.Generic;
using TrampolineMap = System.Collections.Generic.Dictionary<long, System.Reflection.MethodBase>;
using DetourMap = System.Collections.Generic.Dictionary<long, MonoMod.RuntimeDetour.Detour>;
using System.Reflection.Emit;
using System.Linq.Expressions;
using MonoMod.RuntimeDetour;
using Eluant;
using ETGMod;

namespace Stock {
    public class RuntimeHooks {
        private static TrampolineMap _Trampolines = new TrampolineMap();
        private static DetourMap _Detours = new DetourMap();
        private static HashSet<long> _DispatchHandlers = new HashSet<long>();

        private static Logger _Logger = new Logger("RuntimeHooks");
        private static MethodInfo _HandleDispatchMethod = typeof(RuntimeHooks).GetMethod("_HandleDispatch", BindingFlags.Static | BindingFlags.NonPublic);

        private static Dictionary<long, LuaFunction> _Hooks = new Dictionary<long, LuaFunction>();
        private static Dictionary<long, System.Type> _HookReturns = new Dictionary<long, System.Type>();

        public static void DeleteDetour(long token) {
            Detour detour;
            if (_Detours.TryGetValue(token, out detour)) {
                _Logger.Debug($"Deleting detour token {token}");
                detour.Undo();
                detour.Free();
                _Detours.Remove(token);

                if (_Trampolines.ContainsKey(token)) {
                    _Trampolines.Remove(token);
                }

                _DispatchHandlers.Remove(token);
            }
        }

        public static void DeleteAllDetours() {
            foreach (var token in _DispatchHandlers) {
                _Logger.Debug($"Deleting detour token {token}");

                Detour detour;
                if (_Detours.TryGetValue(token, out detour)) {
                    detour.Undo();
                    detour.Free();
                    _Detours.Remove(token);

                    if (_Trampolines.ContainsKey(token))
                    {
                        _Trampolines.Remove(token);
                    }
                }
            }
            _DispatchHandlers.Clear();
        }

        public static long MethodToken(MethodBase method)
            => (long)((ulong)method.MetadataToken) << 32 | (
                (uint)((method.Module.Name.GetHashCode() << 5) + method.Module.Name.GetHashCode()) ^
                (uint)method.Module.Assembly.FullName.GetHashCode());

        public static void InstallDispatchHandler(MethodBase method) {
            var method_token = MethodToken(method);
            if (_DispatchHandlers.Contains(method_token)) {
                _Logger.Debug($"Not installing dispatch handler for {method.Name} ({method_token}) - it's already installed");
                return;
            }

            var parms = method.GetParameters();

            int ptypes_offs = 1;
            if (method.IsStatic) ptypes_offs = 0;

            var ptypes = new Type[parms.Length + ptypes_offs];
            if (!method.IsStatic) ptypes[0] = method.DeclaringType;
            for (int i = 0; i < parms.Length; i++) {
                ptypes[i + ptypes_offs] = parms[i].ParameterType;
            }

            var method_returns = false;
            if (method is MethodInfo) {
                if (((MethodInfo)method).ReturnType != typeof(void)) method_returns = true;
            }

            var dm = new DynamicMethod(
                $"DISPATCH HANDLER FOR METHOD TOKEN {method_token}",
                method is MethodInfo ? ((MethodInfo)method).ReturnType : typeof(void),
                ptypes,
                method.Module,
                skipVisibility: true
            );

            var il = dm.GetILGenerator();

            var loc_args = il.DeclareLocal(typeof(object[]));
            loc_args.SetLocalSymInfo("args");

            var loc_target = il.DeclareLocal(typeof(object));
            loc_target.SetLocalSymInfo("target");

            if (method.IsStatic) {
                il.Emit(OpCodes.Ldnull);
            } else {
                il.Emit(OpCodes.Ldarg_0);
            }
            il.Emit(OpCodes.Stloc, loc_target);

            int ary_size = ptypes.Length - 1;
            if (method.IsStatic) ary_size = ptypes.Length;

            il.Emit(OpCodes.Ldc_I4, ary_size);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc, loc_args);

            for (int i = 0; i < ary_size; i++) {
                il.Emit(OpCodes.Ldloc, loc_args);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + ptypes_offs);
                if (ptypes[i + ptypes_offs].IsValueType) {
                    il.Emit(OpCodes.Box, ptypes[i + ptypes_offs]);
                }
                il.Emit(OpCodes.Stelem, typeof(object));
            }

            il.Emit(OpCodes.Ldc_I8, method_token);
            il.Emit(OpCodes.Ldloc, loc_target);
            il.Emit(OpCodes.Ldloc, loc_args);

            il.Emit(OpCodes.Call, _HandleDispatchMethod);

            if (!method_returns) il.Emit(OpCodes.Pop);
            if (method_returns && method is MethodInfo && ((MethodInfo)method).ReturnType.IsValueType) {
                il.Emit(OpCodes.Unbox_Any, ((MethodInfo)method).ReturnType);
            }
            il.Emit(OpCodes.Ret);

            var detour = new Detour(
                from: method,
                to: dm
            );

            _Detours[method_token] = detour;
            _Trampolines[method_token] = detour.GenerateTrampoline();
            _DispatchHandlers.Add(method_token);

            _Logger.Debug($"Installed dispatch handler for {method.Name} (token {method_token})");
        }

        private static object _HandleDispatch(long method_token, object target, object[] args) {
            string target_name;
            if (target == null) target_name = "NULL";
            else target_name = $"[{target.GetType()}] {target}";
            _Logger.Debug($"Handling dispatch for {method_token} with target {target_name} and {args.Length} argument(s)");
            for (int i = 0; i < args.Length; i++) {
                _Logger.DebugIndent(args[i]);
            }

            bool returned;
            var obj = TryRun(Stock.LuaState, method_token, target, args, out returned);
            if (returned) {
                _Logger.Debug($"Short circuit - hook returned");
                return obj;
            }

            MethodBase trampoline;
            if (_Trampolines.TryGetValue(method_token, out trampoline)) {
                object[] targs;
                if (target == null) {
                    targs = new object[args.Length];
                    args.CopyTo(targs, 0);
                } else {
                    targs = new object[args.Length + 1];
                    targs[0] = target;
                    args.CopyTo(targs, 1);
                }

                return trampoline.Invoke(target, targs);
            }

            throw new Exception("Tried to handle dispatch without a trampoline having been set up");
        }

        public static void Dispose() {
            foreach (var kv in _Hooks) {
                kv.Value.Dispose();
                RuntimeHooks.DeleteDetour(kv.Key);
            }
        }

        private static MethodInfo _TryFindMethod(Type type, string name, Type[] argtypes, bool instance, bool @public) {
            BindingFlags binding_flags = 0;

            if (instance) binding_flags |= BindingFlags.Instance;
            else binding_flags |= BindingFlags.Static;

            if (@public) binding_flags |= BindingFlags.Public;
            else binding_flags |= BindingFlags.NonPublic;

            // not sure if this is needed
            if (argtypes == null) return type.GetMethod(name, binding_flags);
            else return type.GetMethod(name, binding_flags, null, argtypes, null);

        }


        public static void Add(LuaTable details, LuaFunction fn) {
            Type criteria_type;
            string criteria_methodname;
            Type[] criteria_argtypes = null;
            bool criteria_instance;
            bool criteria_public;
            bool hook_returns;

            using (var ftype = details["type"]) {
                if (ftype == null) {
                    throw new LuaException($"type: Expected Type, got null");
                }
                if (!(ftype is IClrObject)) {
                    throw new LuaException($"type: Expected CLR Type object, got non-CLR object of type {ftype.GetType()}");
                }
                else if (!(((IClrObject)ftype).ClrObject is Type)) {
                    throw new LuaException($"type: Expected CLR Type object, got CLR object of type {((IClrObject)ftype).ClrObject.GetType()}");
                }

                criteria_type = ((IClrObject)ftype).ClrObject as Type;
                using (var method = details["method"] as LuaString)
                using (var instance = details["instance"] as LuaBoolean)
                using (var @public = details["public"] as LuaBoolean)
                using (var returns = details["returns"] as LuaBoolean)
                using (var args = details["args"] as LuaTable) {
                    if (method == null) throw new LuaException("method: Expected string, got null");

                    if (args != null) {
                        var count = 0;
                        while (true) {
                            using (var value = args[count + 1]) {
                                if (value is LuaNil) break;
                                if (!(value is IClrObject)) {
                                    throw new LuaException($"args: Expected entry at index {count} to be a CLR Type object, got non-CLR object of type {value.GetType()}");
                                }
                                else if (!(((IClrObject)value).ClrObject is Type)) {
                                    throw new LuaException($"args: Expected entry at index {count} to be a CLR Type object, got CLR object of type {((IClrObject)value).ClrObject.GetType()}");
                                }
                            }
                            count += 1;
                        }

                        var argtypes = new Type[args.Count];

                        for (int i = 1; i <= count; i++) {
                            using (var value = args[i]) {
                                argtypes[i - 1] = (Type)args[i].CLRMappedObject;
                            }
                        }

                        criteria_argtypes = argtypes;
                    }

                    criteria_instance = instance?.ToBoolean() ?? true;
                    criteria_public = @public?.ToBoolean() ?? true;
                    criteria_methodname = method.ToString();

                    hook_returns = returns?.ToBoolean() ?? false;
                }
            }

            var method_info = _TryFindMethod(
                criteria_type,
                criteria_methodname,
                criteria_argtypes,
                criteria_instance,
                criteria_public
            );

            if (method_info == null) {
                throw new LuaException($"Method '{criteria_methodname}' in '{criteria_type.FullName}' not found.");
            }

            RuntimeHooks.InstallDispatchHandler(method_info);

            var token = RuntimeHooks.MethodToken(method_info);
            _Hooks[token] = fn;
            fn.DisposeAfterManagedCall = false;

            if (hook_returns) {
                _HookReturns[token] = method_info.ReturnType;
            }

            _Logger.Debug($"Added Lua hook for method '{criteria_methodname}' ({token})");
        }

        internal static object TryRun(LuaRuntime runtime, long token, object target, object[] args, out bool returned) {
            _Logger.Debug($"Trying to run method hook (token {token})");
            returned = false;

            object return_value = null;

            LuaFunction fun;
            if (_Hooks.TryGetValue(token, out fun)) {
                _Logger.Debug($"Hook found");
                // target == null --> static

                var objs_offs = 1;
                if (target == null) objs_offs = 0;

                var objs = new LuaValue[args.Length + objs_offs];
                if (target != null) objs[0] = runtime.AsLuaValue(target);
                for (int i = 0; i < args.Length; i++) {
                    objs[i + objs_offs] = runtime.AsLuaValue(args[i]);
                }

                var result = fun.Call(args: objs);

                Type return_type;
                if (_HookReturns.TryGetValue(token, out return_type)) {
                    if (result.Count > 1) {
                        for (int i = 1; i < result.Count; i++) result[i].Dispose();
                    }

                    if (result.Count > 0) {
                        returned = true;
                        return_value = runtime.ToClrObject(result[0], return_type);

                        if (return_value != result[0]) result[0].Dispose();
                    }
                }
                else {
                    result.Dispose();
                }

                for (int i = 0; i < objs.Length; i++) objs[i]?.Dispose();
            }
            else _Logger.Debug($"Hook not found");

            return return_value;
        }
    }
}