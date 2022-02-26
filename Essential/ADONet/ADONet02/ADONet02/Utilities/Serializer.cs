using System;
using System.IO;
using System.Xml.Serialization;
using Entities;

namespace Utilities
{
    public static class Serializer
    {
        public static void Serialize(Test test, int id, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (test == null)
                throw new ArgumentNullException(nameof(test));

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                    XmlSerializer serializer = new XmlSerializer(typeof(Test));

                    serializer.Serialize(stream, test);
            }
        }

        public static Test Deserialize(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new ArgumentException($"Файла {filePath} не существует.");

            Test test;
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                    XmlSerializer serializer = new XmlSerializer(typeof(Test));

                    test = (Test)serializer.Deserialize(stream);
            }

            return test;
        }
    }
}
