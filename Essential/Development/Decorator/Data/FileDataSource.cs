using System.IO;
using System.Text;

namespace Data
{
    public class FileDataSource : IDataSource
    {
        private const int codepage = 1251;

        private string name;
        private readonly Encoding encoding;

        public FileDataSource(string name)
        {
            this.name = name;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding = Encoding.GetEncoding(codepage);
        }

        public void WriteData(string data)
        {
            using (FileStream file = new FileStream(name, FileMode.Create, FileAccess.Write))
            {
                byte[] dataBytes = encoding.GetBytes(data);
                file.Write(dataBytes, 0, data.Length);
            }
        }

        public string ReadData()
        {
            byte[] data = null;
            using (FileStream file = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Read))
            {
                data = new byte[file.Length];
                file.Read(data);
            }

            return encoding.GetString(data);
        }
    }
}
