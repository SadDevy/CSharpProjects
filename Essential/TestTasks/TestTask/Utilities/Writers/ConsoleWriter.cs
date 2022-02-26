using System;

namespace Utilities.Writers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}
