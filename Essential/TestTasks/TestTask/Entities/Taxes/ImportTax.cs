namespace Entities.Taxes
{
    public class ImportTax : ITax
    {
        private const string keyWord = "imported";

        private const int defaultTax = 0;
        private const int importTax = 5;

        public int GetPercent(string goodsName)
        {
            bool isImported = goodsName.ToLower().Contains(keyWord);
            if (isImported)
                return importTax;

            return defaultTax;
        }
    }
}
