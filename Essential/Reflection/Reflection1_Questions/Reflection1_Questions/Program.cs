using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Channels;

namespace Reflection1_Questions
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowAllGenericTypes();

            CreateGenericByName();

            Wrapper.CallMethods();
        }

        //Напиши программу, которая находит все Generic типы в сборке (например, mscorlib) и выводит их на экран.
        private static void ShowAllGenericTypes()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var a in types)
            {
                if (a.IsGenericType)
                    Console.WriteLine(a);
            }

        }

        //Напиши программу, которая создает Generic тип по его строковому имени.
        //- Имя Generic типа считывается из конфиг файла приложения.
        //    Например, key=”container” value=”System.Collections.Generic.List<System.Int32>”
        private static void CreateGenericByName()
        {
            string name = ConfigurationManager.AppSettings.Get(0);

            var a = Type.GetType(name, true);
            Console.WriteLine(a);
        }
    }


}
