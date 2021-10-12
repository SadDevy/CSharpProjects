using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Introduction.Data.Html_Helpers
{
    public static class HtmlsHelpers
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper htmlHelper, int imageId, string link)
        {
            var aTag = new TagBuilder("a");
            aTag.InnerHtml.Append(link);

            aTag.Attributes.Add("href", $"images/{imageId}");

            string result = string.Empty;
            using (StringWriter stringWriter = new StringWriter())
            {
                aTag.WriteTo(stringWriter, HtmlEncoder.Default);
                result = stringWriter.ToString();
            }

            return new HtmlString(result);
        }
    }
}
