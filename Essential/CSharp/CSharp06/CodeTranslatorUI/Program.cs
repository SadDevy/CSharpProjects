using System;
using CodeTranslator;

namespace CodeTranslatorUI
{
    class Program
    {
        static void Main()
        {
            const string cSharpName = "CSharp";
            const string vBName = "VB";
            const string codeLine = "something";

            CodeTranslator.IConvertible[] converters = CreateConverters();

            foreach (CodeTranslator.IConvertible element in converters)
            {
                if (element is ICodeChecker)
                {
                    ICodeChecker codeChecker = (ICodeChecker)element;

                    if (codeChecker.CheckCodeSyntax(codeLine, cSharpName))
                    {
                        string cSharpConverted = element.ConvertToCSharp(codeLine);
                        Console.WriteLine("{0} to {1}: {2}", codeLine, cSharpName, cSharpConverted);
                    }

                    if (codeChecker.CheckCodeSyntax(codeLine, vBName))
                    {
                        string vBConverted = element.ConvertToVB(codeLine);
                        Console.WriteLine("{0} to {1}: {2}", codeLine, vBName, vBConverted);
                    }
                }
                else
                {
                    string cSharpConverted = element.ConvertToCSharp(codeLine);
                    Console.WriteLine("{0} to {1}: {2}", codeLine, cSharpName, cSharpConverted);

                    string vBConverted = element.ConvertToVB(codeLine);
                    Console.WriteLine("{0} to {1}: {2}", codeLine, vBName, vBConverted);
                }
            }
        }

        private static CodeTranslator.IConvertible[] CreateConverters()
        {
            return new CodeTranslator.IConvertible[]
            {
                new ProgramConverter(),
                new ProgramHelper()
            };
        }
    }
}
