using System;

namespace Entities.Formatters
{
    public class HtmlStatementFormatter : IStatementFormatter
    {
        public string GetHeader(string name) => string.Format("<H1>Rentals for <EM>{0}</EM></H1><P>{1}", name, Environment.NewLine);

        public string GetBody(Rental rental) => string.Format("{0}: {1}<BR>{2}", rental.Movie.Title, rental.GetCharge(), Environment.NewLine);

        public string GetFooter(double totalCharge, int totalFrequentRentalPoints)
        {
            string result = string.Empty;
            result += string.Format("<P>You owe <EM>{0}</EM><P>{1}", totalCharge, Environment.NewLine);
            result += string.Format("On this rental you earned <EM>{0}</EM> frequent renter points<P>", totalFrequentRentalPoints);

            return result;
        }
    }
}
