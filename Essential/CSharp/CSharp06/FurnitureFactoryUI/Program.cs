using System;
using FurnitureFactory.Furnitures.Common;
using FurnitureFactory.Furnitures.Modern;
using FurnitureFactory.Furnitures.Victorian;

namespace FurnitureFactoryUI
{
    class Program
    {
        static void Main()
        {
            IFurnitureFactory[] factories = CreateFactories();
            foreach(IFurnitureFactory factory in factories)
            {
                Client client = new Client(factory);
                Console.WriteLine(client);
            }
        }

        private static IFurnitureFactory[] CreateFactories()
        {
            return new IFurnitureFactory[]
            {
                new VictorianFurnitureFactory(),
                new ModernFurnitureFactory()
            };
        }
    }
}