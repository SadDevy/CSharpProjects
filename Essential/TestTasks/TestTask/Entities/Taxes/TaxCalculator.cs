using System;
using System.Collections.Generic;

namespace Entities.Taxes
{
    public static class TaxCalculator
    {
        private const decimal multipleValue = 0.05m;

        public static decimal CalculateTax(Goods goods, IEnumerable<ITax> taxes)
        {
            int taxPercent = GetTaxPercent(goods, taxes);

            return CalculateTax(goods.Price, taxPercent);
        }

        private static decimal CalculateTax(decimal price, int taxPercent)
        {
            if (taxPercent == 0)
                return 0;

            decimal tax = price * (decimal)(taxPercent / 100.0);
            return RoundTax(tax);
        }

        private static decimal RoundTax(decimal taxValue)
        {
            return Math.Ceiling(taxValue / multipleValue) * multipleValue;

        }

        private static int GetTaxPercent(Goods goods, IEnumerable<ITax> taxes)
        {
            int percent = 0;
            foreach (ITax tax in taxes)
                percent += tax.GetPercent(goods.Name);

            return percent;
        }
    }
}
