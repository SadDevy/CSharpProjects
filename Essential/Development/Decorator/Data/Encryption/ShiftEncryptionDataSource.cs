namespace Data.Encryption
{
    public class ShiftEncryptionDataSource : DataSourceDecorator
    {
        private const string russianAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string englishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly int offset;

        public ShiftEncryptionDataSource(IDataSource source, int offset) : base(source) => this.offset = offset;

        public override void WriteData(string data) => base.WriteData(EncodeCezar(data, offset));

        public override string ReadData() => EncodeCezar(base.ReadData(), -offset);

        private string EncodeCezar(string data, int offset)
        {
            string result = string.Empty;
            foreach (char symbol in data)
                result += EncodeSymbol(symbol, offset);

            return result;
        }

        private char EncodeSymbol(char symbol, int offset)
        {
            string alphabet = GetAlphabet(symbol);
            return (alphabet == string.Empty) ? symbol : EncodeSymbol(alphabet, symbol, offset);
        }

        private string GetAlphabet(char symbol)
        {
            string alphabet = string.Empty;
            if (russianAlphabet.Contains(symbol))
                alphabet = russianAlphabet;

            if (russianAlphabet.ToLower().Contains(symbol))
                alphabet = russianAlphabet.ToLower();

            if (englishAlphabet.Contains(symbol))
                alphabet = englishAlphabet;

            if (englishAlphabet.ToLower().Contains(symbol))
                alphabet = englishAlphabet.ToLower();

            return alphabet;
        }

        private char EncodeSymbol(string alphabet, char symbol, int offset)
        {
            int position = alphabet.IndexOf(symbol);
            int codePosition = (alphabet.Length + position + offset) % alphabet.Length;

            return alphabet[codePosition];
        }
    }
}
