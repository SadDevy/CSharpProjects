namespace CreditGiver
{
    public class CreditBureauChecker
    {
        private readonly string personName;

        public CreditBureauChecker(string personName)
        {
            this.personName = personName;
        }

        public bool CreditHistoryIsGood()
        {
            return true;
        }

        public bool ThereIsNoUnpayed()
        {
            return true;
        }
    }
}
