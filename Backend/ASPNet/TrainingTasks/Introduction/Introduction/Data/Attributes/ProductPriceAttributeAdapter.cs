using System.Globalization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace Introduction.Data.Attributes
{
    public class ProductPriceAttributeAdapter : AttributeAdapterBase<ProductPriceAttribute>
    {
        public ProductPriceAttributeAdapter(ProductPriceAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer) { }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-productPrice", GetErrorMessage(context));

            string price = Attribute.Price.ToString(CultureInfo.InvariantCulture);
            MergeAttribute(context.Attributes, "data-val-productPrice-price", price);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
            => Attribute.GetErrorMessage();
    }
}
