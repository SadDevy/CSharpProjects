using System;
using System.Text;
using System.Threading;

namespace MessagePublisherUI
{
    public class VKPublisher : Network
    {
        public VKPublisher(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public override bool LogIn(string userName, string password)
        {
            Console.WriteLine("Checking user's parameters");
            Console.WriteLine("Name: " + userName);
            Console.WriteLine("Password:");
            Console.WriteLine();
            for (int i = 0; i < password.Length; i++)
            {
                Console.Write("*");
            }

            SimulateNerworkLatency();

            Console.WriteLine("LogIn success on VK");
            return true;
        }

        public override void LogOut()
        {
            Console.WriteLine("User " + userName + " was logged out from VK");
        }

        public override bool SendData(byte[] data)
        {
            bool messagePosted = true;
            if (messagePosted)
            {
                string message = Encoding.UTF8.GetString(data);
                Console.WriteLine("Message " + message + " was posted on VK");
            }

            return false;
        }

        private void SimulateNerworkLatency()
        {
            try
            {
                int i = 0;
                Console.WriteLine();
                while (i < 10)
                {
                    Console.Write(".");
                    Thread.Sleep(500);
                    i++;
                }
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
