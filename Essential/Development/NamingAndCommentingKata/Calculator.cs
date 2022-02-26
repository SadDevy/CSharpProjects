namespace NamingAndCommenting
{
    public class Calculator
    {
        public double CalculateDiscount(int productCount, double price)
        {
            if (IsRetailPurchase(productCount))
                return 0;

            if (IsCashAndCarryPurchase(productCount))
                return ApplyCashAndCarryDiscount(productCount, price);

            return ApplyWholeSaleDiscount(productCount, price);
        }

        private static bool IsCashAndCarryPurchase(int productCount)
        {
            const int boughtProducts = 11;
            return productCount < boughtProducts;
        }

        private static bool IsRetailPurchase(int productCount)
        {
            const int boughtProducts = 3;
            return productCount < boughtProducts;
        }

        private double ApplyCashAndCarryDiscount(int productCount, double price)
        {
            const int oneForFreeForEvery = 3;
            int amountProductsForFree = productCount / oneForFreeForEvery;
            return amountProductsForFree * price;
        }

        private double ApplyWholeSaleDiscount(int productCount, double price)
        {
            const decimal discountPercent = 50;
            return (double)(productCount * discountPercent * (decimal)price / 100); 
        }
    }
}
