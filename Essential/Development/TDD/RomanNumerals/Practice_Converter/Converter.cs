using System;
using Numerals;

namespace Practice_Converter
{
    public static class Converter
    {
        public static string ConvertArabicToRoman(uint arabicNumber)
        {
            string romanNumber = string.Empty;

            Numeral[] numerals = {
                new Numeral(1000, "M"),
                new Numeral(900, "CM"),
                new Numeral(500, "D"), 
                new Numeral(400, "CD"),
                new Numeral(100, "C"),
                new Numeral(90, "XC"),
                new Numeral(40, "XL"),
                new Numeral(10, "X"),
                new Numeral(9, "IX"),
                new Numeral(5, "V"),
                new Numeral(4, "IV"),
                new Numeral(1, "I")
            };

            foreach (Numeral numeral in numerals)
            {
                for (; arabicNumber >= numeral.Number; arabicNumber -= numeral.Number)
                {
                    romanNumber += numeral.Num;
                }
            }

            return romanNumber;
        }
    }
}
