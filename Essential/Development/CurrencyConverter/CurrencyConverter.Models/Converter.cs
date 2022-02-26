using System;

namespace CurrencyConverter.Models
{
    public class Converter
    {
        private decimal valueDollar = default;
        private decimal valueRuble = default;
        private decimal dollarCourse = default;

        public decimal ValueDollar
        {
            get => valueDollar;
            set
            {
                valueDollar = value;
                valueRuble = valueDollar * dollarCourse;
            }
        }

        public decimal ValueRuble
        {
            get => valueRuble;
            set
            {
                valueRuble = value;
                valueDollar = valueRuble / dollarCourse;
            }
        }

        public decimal DollarCource
        {
            get => dollarCourse;
            set => dollarCourse = value;
        }
    }
}
