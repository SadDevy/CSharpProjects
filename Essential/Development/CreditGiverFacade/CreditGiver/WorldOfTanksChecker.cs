namespace CreditGiver
{
    public class WorldOfTanksChecker
    {
        private readonly string personName;
        public WorldOfTanksChecker(string personName)
        {
            this.personName = personName;
        }

        public bool WasOnlineMoreThanMonthAgo()
        {
            return true;
        }
    }
}
