namespace CodeTranslator
{
    public class ProgramConverter : IConvertible
    {
        public string ConvertToCSharp(string code)
        {
            return "To CSharp";
        }

        public string ConvertToVB(string code)
        {
            return "To VB";
        }
    }
}
