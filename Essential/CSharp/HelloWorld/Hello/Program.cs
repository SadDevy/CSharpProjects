using System;

namespace Hello
{
    /// <summary>
    /// Отображает приветствие в консоли.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Отображает приветствие пользователя.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Hello {0}!", Environment.UserName);
        }
    }
}
