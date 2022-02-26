namespace CodeTranslator
{
    public interface IConvertible
    {

        public string ConvertToCSharp(string code);

        public string ConvertToVB(string code);
    }
}
