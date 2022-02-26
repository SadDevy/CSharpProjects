using System;
using FurnitureFactories.Furnitures.Common;
using FurnitureFactories.Furnitures.Office;
using FurnitureFactories.Furnitures.School;

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
                new SchoolFurnitureFactory(),
                new GeneralFurnitureFactory<SchoolFurniture, SchoolChair, SchoolSofa>(),
                new GeneralFurnitureFactory<OfficeFurniture, OfficeChair, OfficeSofa>()
            };
        }
    }
}