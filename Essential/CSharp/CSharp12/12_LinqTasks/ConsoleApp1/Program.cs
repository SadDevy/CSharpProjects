using LinqProvider;
using System.Dynamic;
using System.Linq;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = { "bg", "beree", "bbeeee", "hello", "worlds", "eeeeee" };

            var f = Provider.CreatePredicate('e');

            //var c = "beebeee".GetSymbols(n => f(n));

            ////var c = "beebee".Select(n => f(n));

            //foreach (var a in data.GetSymbols(n => f(n)))
            //    foreach (var d in a)
            //        System.Console.WriteLine(d);

            foreach (var d in "bbeeeebeen".GetSymbols(f))
                System.Console.WriteLine(d);
            //
            //  System.Console.WriteLine(Provider.IsValid("1234"));

            // System.Console.WriteLine();

            //foreach (var c in "beeeebeeseseeee".FindSeries('e'))
            //    System.Console.WriteLine(c);

        }
    }
}
