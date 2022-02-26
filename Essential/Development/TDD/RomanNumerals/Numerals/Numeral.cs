using System;

namespace Numerals
{
    public struct Numeral
    {
        public uint Number
        {
            get; set;
        }

        public string Num
        {
            get; set;
        }

        public Numeral(uint number, string numeral)
        {
            Number = number;
            Num = numeral;
        }
    }
}
