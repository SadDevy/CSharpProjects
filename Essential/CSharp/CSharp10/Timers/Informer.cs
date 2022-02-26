using System;
using System.Collections.Generic;
using System.Text;

namespace Timers
{
    public static class Informer
    {
        public static void Show(string messageFormat, params object[] values)
        {
            string message = string.Format(messageFormat, values);
            Show(message);
        }

        public static void Show(string message)
        {
            Console.WriteLine("[{0}]:Timer: {1}", DateTime.Now.Second, message);
        }
    }
}
