using System;

namespace Entities.Formatters
{
    public class DefaultStatementFormatter : IStatementFormatter
    {
        public string GetHeader(string name) =>string.Format("Rental Record for {0}{1}", name, Environment.NewLine);

        public string GetBody(Rental rental) => string.Format("{0} {1}{2}", rental.Movie.Title, rental.GetCharge(), Environment.NewLine);

        public string GetFooter(double totalCharge, int totalFrequentRenterPoints)
        {
            string result = string.Empty;
            result += string.Format("Amount owed is {0}{1}", totalCharge, Environment.NewLine);
            result += string.Format("You earned {0} frequent renter points", totalFrequentRenterPoints);

            return result;
        }
    }
}
