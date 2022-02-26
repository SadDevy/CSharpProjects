using System.Text;

namespace Data
{
    public class StringDataSource : IDataSource
    {
        private const int codepage = 1251;

        private byte[] data;
        private readonly Encoding encoding;

        public StringDataSource()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding = Encoding.GetEncoding(codepage);
        }

        public void WriteData(string data) => this.data = encoding.GetBytes(data);

        public string ReadData() => encoding.GetString(data);
    }
}
