using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Introduction.Data.Tag_Helpers
{
    [HtmlTargetElement(Attributes = "northwind-id")]
    public class NorthwindIdTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.TryGetAttribute("northwind-id", out TagHelperAttribute attribute);

            output.Attributes.RemoveAll("northwind-id");
            output.Attributes.Add("href", $"images/{attribute.Value}");
        }
    }
}
