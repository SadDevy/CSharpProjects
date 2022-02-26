namespace CodeTranslator
{
    public class ProgramHelper : ProgramConverter, ICodeChecker
    {
        public bool CheckCodeSyntax(string code, string language)
        {
            return code == language;
        }
    }
}