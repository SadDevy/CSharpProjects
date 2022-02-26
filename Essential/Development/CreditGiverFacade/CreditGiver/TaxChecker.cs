namespace CreditGiver
{
    public static class TaxChecker
    {
        private static decimal payedForLastYear = 1000.15m;

        public static bool GreaterThanPayedForLastYear(decimal creditSum)
        {
            return creditSum > payedForLastYear;
        }
    }
}
