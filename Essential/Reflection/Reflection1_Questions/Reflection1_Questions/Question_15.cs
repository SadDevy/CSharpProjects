using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection1_Questions
{
    // Напиши класс wrapper (обертку), которая настраивается на некоторый объект определенного типа, но вызывает его методы через Reflection (2..3 операции).
    public static class Wrapper
    {
        public static void CallMethods()
        {
            Type type = typeof(Provider);

            object instance = Activator.CreateInstance(type);

            IEnumerable<MethodInfo> methods = type.GetTypeInfo().DeclaredMethods;
            methods.First().Invoke(instance, new object?[0]);
            methods.Last().Invoke(instance, new object?[]{ 1, 2 });
        }
    }

    public class Provider
    {
        public void SayHelloWorld()
        {
            Console.WriteLine("HelloWorld!");
        }

        public void SumAndShow(int a, int b)
        {
            int sum = a + b;
            Console.WriteLine(sum);
        }
    }
}
