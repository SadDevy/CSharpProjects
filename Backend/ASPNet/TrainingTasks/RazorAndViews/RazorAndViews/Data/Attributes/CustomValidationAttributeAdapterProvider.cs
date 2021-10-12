using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace Introduction.Data.Attributes
{
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider =
            new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is ProductPriceAttribute productPriceAttribute)
                return new ProductPriceAttributeAdapter(productPriceAttribute, stringLocalizer);

            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
