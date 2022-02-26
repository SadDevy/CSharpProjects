using System;
using Utilities;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Exporter exporter = new Exporter();

            //exporter.ExportTestToXml(18, "7.xml");

            //Importer importer = new Importer();
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");
            //importer.ImportTestFromXml("7.xml");

            Remover remover = new Remover();
            remover.RemoveTestFromDb(new Guid("2d102ec9-4fd1-0000-b65a-a28c15325712"));
        }
    }
}
