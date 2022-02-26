using System;

namespace LockSatementUI
{
    public class ConsoleViewer : IView
    {
        public void Show(string message)
        {
            Console.WriteLine(message);
        }
    }
}
