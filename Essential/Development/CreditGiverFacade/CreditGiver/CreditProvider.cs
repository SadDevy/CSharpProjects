namespace CreditGiver
{
    public class CreditProvider
    {
        public bool GiveCredit(string personName, decimal creditSum)
        {
            CreditBureauChecker creditDepartmentChecker = new CreditBureauChecker(personName);
            LabourMinistryChecker workDepartmentChecker = new LabourMinistryChecker(personName);
            WorldOfTanksChecker worldOfTanksChecker = new WorldOfTanksChecker(personName);

            return creditDepartmentChecker.CreditHistoryIsGood() 
                   && creditDepartmentChecker.ThereIsNoUnpayed() 
                   && TaxChecker.GreaterThanPayedForLastYear(creditSum)
                   && workDepartmentChecker.IsEmployee() 
                   && worldOfTanksChecker.WasOnlineMoreThanMonthAgo();
        }
    }
}
