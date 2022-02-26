using NUnit.Framework;
using Entities.Prices;

namespace EntitiesTests.PricesTests
{
    public abstract class PriceTests
    {
        public abstract Price CreatePrice();

        public virtual int TestGetPriceCode()
        {
            Price price = CreatePrice();
            Assert.IsNotNull(price);

            int actual = price.GetPriceCode();

            return actual;
        }

        public virtual double TestGetCharge(int daysRented)
        {
            Price price = CreatePrice();
            Assert.IsNotNull(price);

            double actual = price.GetCharge(daysRented);

            return actual;
        }

        public virtual int TestGetFrequentRenterPoints(int daysRented)
        {
            Price price = CreatePrice();
            Assert.IsNotNull(price);

            int actual = price.GetFrequentRenterPoints(daysRented);

            return actual;
        }
    }
}
