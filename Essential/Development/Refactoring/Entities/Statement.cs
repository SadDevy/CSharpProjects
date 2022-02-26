using System.Collections.Generic;
using Entities.Formatters;

namespace Entities
{
    public class Statement
    {
        private IStatementFormatter formatter;

        public Statement(IStatementFormatter formatter) => this.formatter = formatter;

        public string GetStatement(string name, IEnumerable<Rental> rentals, double totalCharge, int totalFrequentRentalPoints)
            => formatter.GetHeader(name) + GetBody(rentals) + formatter.GetFooter(totalCharge, totalFrequentRentalPoints);

        private string GetBody(IEnumerable<Rental> rentals)
        {
            IEnumerator<Rental> enumerator = rentals.GetEnumerator();
            string result = string.Empty;
            while (enumerator.MoveNext())
            {
                Rental each = enumerator.Current;

                result += formatter.GetBody(each);
            }

            return result;
        }
    }
}
