namespace CreditGiver
{
    public class LabourMinistryChecker
    {
        private readonly string personName;
        public LabourMinistryChecker(string personName)
        {
            this.personName = personName;
        }

        public bool IsEmployee()
        {
            return true;
        }
    }
}
