using System;
using System.Reflection;
using Eluant;

namespace Stock {
    public class NamespaceHelper {
        private Assembly Assembly;
        private string Namespace;

        public NamespaceHelper(Assembly ass, string @namespace) {
            Assembly = ass;
            Namespace = @namespace;
        }

        public NamespaceHelper @namespace(string path) {
            var namespace_exists = false;
            var new_namespace = $"{Namespace}.{path}";

            var types = Assembly.GetTypes();
            for (int i = 0; i < types.Length; i++) {
                if (string.Equals(types[i].Namespace, new_namespace, StringComparison.Ordinal)) {
                    namespace_exists = true;
                    break;
                }
            }

            if (!namespace_exists) throw new LuaException($"Namespace '{new_namespace}' doesn't exist.");

            return new NamespaceHelper(Assembly, new_namespace);
        }
           
        public LuaClrTypeObject type(string path) {
            return Stock.LuaState.ClrType(Assembly, $"{Namespace}.{path}");
        }

        public LuaClrTypeObject generic_type(string path, LuaTable types) {
            path = $"{Namespace}.{path}`{types.Count}";
            var type = Assembly.GetType(path);
            if (type == null) throw new LuaException($"Type '{path}' doesn't exist");
            return AssemblyHelper.MakeGenericType(type, types);
        }

        public LuaClrTypeObject array_type(string path, int dimensions = 1) {
            path = $"{Namespace}.{path}";
            var type = Assembly.GetType(path);
            if (type == null) throw new LuaException($"Type '{path}' doesn't exist");

            return AssemblyHelper.MakeArrayType(type, dimensions);
        }

        public LuaTable all_types() {
            return Stock.LuaState.ClrNamespace(Assembly, Namespace);
        }
    }

    public class AssemblyHelper {
        private Assembly Assembly;

        public AssemblyHelper(string name) {
            Assembly = Assembly.Load(name);
        }

        public NamespaceHelper @namespace(string path) {
            var namespace_exists = false;

            var types = Assembly.GetTypes();
            for (int i = 0; i < types.Length; i++) {
                if (string.Equals(types[i].Namespace, path, StringComparison.Ordinal)) {
                    namespace_exists = true;
                    break;
                }
            }

            if (!namespace_exists) throw new LuaException($"Namespace '{path}' doesn't exist.");

            return new NamespaceHelper(Assembly, path);
        }

        public LuaClrTypeObject type(string path) {
            return Stock.LuaState.ClrType(Assembly, path);
        }

        internal static LuaClrTypeObject MakeArrayType(Type type, int dimensions) {
            for (int i = 0; i < dimensions; i++) {
                type = type.MakeArrayType();
            }
            return new LuaClrTypeObject(type);
        }

        public LuaClrTypeObject array_type(string path, int dimensions = 1) {
            var type = Assembly.GetType(path);
            if (type == null) throw new LuaException($"Type '{path}' doesn't exist");

            return MakeArrayType(type, dimensions);
        }

        internal static LuaClrTypeObject MakeGenericType(Type type, LuaTable generic_args) {
            if (generic_args == null) throw new LuaException("Type table (arg #2) must not be nil");
            var count = 0;
            while (true) {
                using (var value = generic_args[count + 1]) {
                    if (value is LuaNil) break;
                    if (!(value is IClrObject)) {
                        throw new LuaException($"types: Expected entry at index {count} to be a CLR Type object, got non-CLR object of type {value.GetType()}");
                    }
                    else if (!(((IClrObject)value).ClrObject is Type)) {
                        throw new LuaException($"types: Expected entry at index {count} to be a CLR Type object, got CLR object of type {((IClrObject)value).ClrObject.GetType()}");
                    }
                }
                count += 1;
            }

            var clr_generic_args = new Type[generic_args.Count];

            for (int i = 1; i <= count; i++) {
                using (var value = generic_args[i]) {
                    clr_generic_args[i - 1] = (Type)generic_args[i].CLRMappedObject;
                }
            }

            return new LuaClrTypeObject(type.MakeGenericType(clr_generic_args));
        }

        public LuaClrTypeObject generic_type(string path, LuaTable types) {
            path = $"{path}`{types.Count}";
            var type = Assembly.GetType(path);
            if (type == null) throw new LuaException($"Type '{path}' doesn't exist");
            return MakeGenericType(type, types);
        }
    }
}
