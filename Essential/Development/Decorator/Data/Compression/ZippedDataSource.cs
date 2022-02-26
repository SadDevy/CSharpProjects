using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Data.Compression
{
    public class ZippedDataSource : DataSourceDecorator
    {
        private const int codepage = 1251;
        private readonly Encoding encoding;

        public ZippedDataSource(IDataSource source) : base(source)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding = Encoding.GetEncoding(codepage);
        }

        public override void WriteData(string data) => base.WriteData(Compress(data));

        public override string ReadData() => Decompress(base.ReadData());

        private string Compress(string stringData)
        {
            byte[] data = encoding.GetBytes(stringData);

            MemoryStream memory;
            using (memory = new MemoryStream())
            using (GZipStream gZip = new GZipStream(memory, CompressionMode.Compress))
            {
                gZip.Write(data);
            }

            return Convert.ToBase64String(memory.ToArray());
        }

        private string Decompress(string stringData)
        {
            byte[] data = Convert.FromBase64String(stringData);

            MemoryStream result;
            using (MemoryStream memory = new MemoryStream(data))
            using (GZipStream gZip = new GZipStream(memory, CompressionMode.Decompress))
            using (result = new MemoryStream())
            {
                int a;
                while ((a = gZip.ReadByte()) > 0)
                {
                    result.WriteByte((byte)a);
                }
            }

            return encoding.GetString(result.ToArray());
        }

    }
}
