using Entities;
using Entities.Taxes;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Utilities.Formatters
{
    public class DefaultFormatter : IFormatter
    {
        const string headerFormat = "Output {0}:";

        private readonly ITax[] taxes;
        public DefaultFormatter()
        {
            taxes = new ITax[]
            {
                new BasicTax(),
                new ImportTax()
            };
        }

        public string Format(Basket basket, int outputNumber)
        {
            if (basket == null)
                return null;

            StringBuilder builder = new StringBuilder();

            string header = string.Format(headerFormat, outputNumber);
            builder.AppendLine(header);

            foreach (Goods goods in basket.Goods)
            {
                string formattedGoods = GetFormattedGoods(goods);
                builder.AppendLine(formattedGoods);
            }

            string formattedSalesTaxes = GetFormattedSalesTaxes(basket.Goods);
            builder.AppendLine(formattedSalesTaxes);

            string formattedTotalPrice = GetFormattedTotalPrice(basket.Goods);
            builder.AppendLine(formattedTotalPrice);

            return builder.ToString();
        }

        private string GetFormattedGoods(Goods goods)
        {
            const string format = "{0}: {1:#,##0.0#}";

            decimal tax = TaxCalculator.CalculateTax(goods, taxes);
            decimal price = goods.Price + tax;

            return string.Format(CultureInfo.InvariantCulture, format, goods.Name, price);
        }

        private string GetFormattedSalesTaxes(IEnumerable<Goods> goods)
        {
            const string format = "Sales Taxes: {0:#,##0.0#}";

            decimal tax = goods.Sum(n => 
                TaxCalculator.CalculateTax(n, taxes));
            
            return string.Format(CultureInfo.InvariantCulture, format, tax);
        }

        private string GetFormattedTotalPrice(IEnumerable<Goods> goods)
        {
            const string format = "Total: {0:#,##0.0#}";

            decimal total = goods.Sum(n => 
                n.Price + TaxCalculator.CalculateTax(n, taxes));

            return string.Format(CultureInfo.InvariantCulture, format, total);
        }
    }
}
