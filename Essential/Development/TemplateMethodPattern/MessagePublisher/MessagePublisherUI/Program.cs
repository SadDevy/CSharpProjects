using System;

namespace MessagePublisherUI
{
    class Program
    {
        static void Main()
        {
            Network network = null;

            Console.WriteLine("Input user name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Input password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Input message: ");
            string message = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Choose social network for posting message.");
            Console.WriteLine("1 - Facebook");
            Console.WriteLine("2 - Twitter");
            Console.WriteLine("3 - VK");
            Console.WriteLine("4 - OK");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: network = new FacebookPublisher(userName, password); break;
                case 2: network = new TwitterPublisher(userName, password); break;
                case 3: network = new VKPublisher(userName, password); break;
                case 4: network = new OKPublisher(userName, password); break;
            }

            network.Post(message);
        }
    }
}
