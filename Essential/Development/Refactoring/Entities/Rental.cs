namespace Entities
{
    public class Rental
    {
        private Movie movie;
        public Movie Movie { get => movie; }

        private int daysRented;
        public int DaysRented { get => daysRented; }

        public Rental(Movie movie, int daysRented = 0)
        {
            this.movie = movie;
            this.daysRented = daysRented;
        }

        public double GetCharge() => movie.GetCharge(daysRented);

        public int GetFrequentRenterPoints() => movie.GetFrequentRenterPoints(daysRented);
    }
}
