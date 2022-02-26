using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            const int value = 5;

            Type[] types = Assembly.Load(new AssemblyName(nameof(BReflection))).GetTypes();
            foreach (Type type in types)
            {
                ShowTypeFullName(type);

                object instance = GetInstance(type);

                SetValueToAllProperties(type, instance, value);
                SetValueToAllFields(type, instance, value);

                ShowResults(type, instance);
            }
        }

        private static void ShowTypeFullName(Type type) => Console.WriteLine(type.FullName);

        private static object GetInstance(Type type)
        {
            const int parametersCount = 1;

            ConstructorInfo ctr = type.GetTypeInfo().DeclaredConstructors
                .FirstOrDefault(n => n.GetParameters().Length == parametersCount
                                     && n.GetParameters()[0].ParameterType == typeof(int));

            return ctr.Invoke(new object[] { 3 });
        }

        private static void SetValueToAllProperties(Type type, object instance, int value)
        {
            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (PropertyInfo p in properties)
                p.SetValue(instance, value);
        }

        private static void SetValueToAllFields(Type type, object instance, int value)
        {
            IEnumerable<FieldInfo> fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo f in fields)
                f.SetValue(instance, value);
        }

        private static void ShowResults(Type type, object instance)
        {
            IEnumerable<MethodInfo> methods = type.GetTypeInfo().DeclaredMethods.Where(n => !n.IsSpecialName);
            foreach (MethodInfo method in methods)
            {
                int result = (int)method.Invoke(instance, null);
                Console.WriteLine(result);
            }
        }
    }
}
