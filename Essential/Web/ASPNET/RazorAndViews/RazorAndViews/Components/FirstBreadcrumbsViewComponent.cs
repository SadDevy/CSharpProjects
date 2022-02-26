using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Introduction.Components
{
    public class FirstBreadcrumbsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            RouteValueDictionary values = HttpContext.Request.RouteValues;

            return View("~/Views/Components/FirstBreadcrumbs.cshtml", values.ToDictionary(k => k.Key, v => v.Value));
        }
    }
}
