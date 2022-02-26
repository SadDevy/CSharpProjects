namespace Entities.Prices
{
    public class NewReleasePrice : Price
    {
        public override double GetCharge(int daysRented) => daysRented * 3;

        public override int GetPriceCode() => Movie.newRelease;

        public override int GetFrequentRenterPoints(int daysRented) => (daysRented > 1) ? 2 : 1;
    }
}
