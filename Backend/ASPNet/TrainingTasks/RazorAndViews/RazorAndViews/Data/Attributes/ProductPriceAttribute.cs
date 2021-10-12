using System.ComponentModel.DataAnnotations;
using Introduction.Models;

namespace Introduction.Data.Attributes
{
    public class ProductPriceAttribute : ValidationAttribute
    {
        public double Price { get; set; }

        public ProductPriceAttribute(double price)
        {
            Price = price;
        }

        public string GetErrorMessage()
        {
            return $"Price should be less than {Price}.";
        }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            Product product = (Product)validationContext.ObjectInstance;
            decimal price = (decimal)value;

            if (price >= (decimal)Price)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
