namespace CodeTranslator
{
    public interface ICodeChecker
    {
        public bool CheckCodeSyntax(string code, string language);
    }
}
