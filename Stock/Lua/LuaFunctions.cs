using System;
using Eluant;

namespace Stock {
    public static class LuaFunctions {
        public static void Hook(LuaTable details, LuaFunction fn) {
            RuntimeHooks.Add(details, fn);
        }

        public static AssemblyHelper Assembly(string name) {
            return new AssemblyHelper(name);
        }

        public delegate LuaClrTypeObject ArrayTypeFuncDelegate(Type type, int dimensions = 1);
        public static LuaClrTypeObject ArrayType(Type type, int dimensions = 1) {
            return AssemblyHelper.MakeArrayType(type, dimensions);
        }

        public static LuaClrTypeObject GenericType(Type type, LuaTable generic_args) {
            return AssemblyHelper.MakeGenericType(type, generic_args);
        }

        public static object Get(LuaVararg args) {
            Console.WriteLine($"ARGS COUNT: {args.Count} {args[0]}");

            if (args.Count < 2) {
                throw new LuaException("Too few arguments (need at least an object and a single index)");
            }
            var target = args[0];

            var index = new object[args.Count - 1];
            for (int i = 0; i < index.Length; i++) {
                index[i] = args[i + 1].CLRMappedObject;
            }
            return target.CLRMappedType.GetProperty("Item").GetValue(target.CLRMappedObject, index);
        }

        public static void Set(LuaVararg args) {
            if (args.Count < 3) {
                throw new LuaException("Too few arguments (need at least an object, a single index and a value)");
            }
            var target = args[0];

            var index = new object[args.Count - 2];
            for (int i = 0; i < index.Length; i++) {
                index[i] = args[i + 1].CLRMappedObject;
            }

            var property = target.CLRMappedType.GetProperty("Item");

            var value = args[args.Count - 1].CLRMappedObject;
            var value_converted = Convert.ChangeType(value, property.PropertyType);

            property.SetValue(target.CLRMappedObject, value_converted, index);
        }
    }
}
