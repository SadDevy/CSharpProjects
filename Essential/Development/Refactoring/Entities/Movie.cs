using Entities.Prices;
using System;

namespace Entities
{
    public class Movie
    {
        public const int childrens = 2;
        public const int regular = 0;
        public const int newRelease = 1;

        private string title;
        public string Title { get => title; }

        private Price price;
        public int PriceCode
        {
            get => price.GetPriceCode();
            set
            {
                switch (value)
                {
                    case regular:
                        price = new RegularPrice();
                        break;
                    case childrens:
                        price = new ChildrensPrice();
                        break;
                    case newRelease:
                        price = new NewReleasePrice();
                        break;
                    default:
                        throw new InvalidOperationException("Incorrect Price Code");
                }
            }
        }

        public Movie(string title, int priceCode = 0)
        {
            this.title = title;
            PriceCode = priceCode;
        }

        public double GetCharge(int daysRented) => price.GetCharge(daysRented);

        public int GetFrequentRenterPoints(int daysRented) => price.GetFrequentRenterPoints(daysRented);
    }
}
