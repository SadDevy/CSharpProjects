using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Entities.Formatters;

[assembly: InternalsVisibleTo("EntitiesTests")]

namespace Entities
{
    public class Customer
    {
        internal List<Rental> _rentals = new List<Rental>();

        private Statement statement;

        private string _name;
        public string Name { get => _name; }

        public Customer(string name, IStatementFormatter formatter)
        {
            _name = name;
            statement = new Statement(formatter);
        }

        public void AddRental(Rental arg) => _rentals.Add(arg);

        public string GetStatement()
        {
            return statement.GetStatement(Name, _rentals, GetTotalCharge(), GetTotalFrequentRenterPoints());
        }

        private double GetTotalCharge()
        {
            double result = 0;
            IEnumerator<Rental> rentals = _rentals.GetEnumerator();
            while (rentals.MoveNext())
            {
                Rental each = rentals.Current;
                result += each.GetCharge();
            }

            return result;
        }

        private int GetTotalFrequentRenterPoints()
        {
            int result = 0;
            IEnumerator<Rental> rentals = _rentals.GetEnumerator();
            while (rentals.MoveNext())
            {
                Rental each = rentals.Current;
                result += each.GetFrequentRenterPoints();
            }

            return result;
        }

    }
}
