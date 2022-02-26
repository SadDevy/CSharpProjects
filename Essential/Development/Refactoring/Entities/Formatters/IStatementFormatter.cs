namespace Entities.Formatters
{
    public interface IStatementFormatter
    {
        public string GetHeader(string name);
        public string GetBody(Rental rental);
        public string GetFooter(double totalCharge, int totalFrequentRentalPoints);
    }
}
