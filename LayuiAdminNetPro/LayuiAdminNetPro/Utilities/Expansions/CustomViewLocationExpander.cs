using Microsoft.AspNetCore.Mvc.Razor;

namespace LayuiAdminNetPro.Utilities.Expansions
{
    /// <summary>
    /// 配置模版视图路径
    /// </summary>
    public class CustomViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string[] locations = { "/Areas/View/Views/{1}/{0}.cshtml", "/Areas/View/Views/{0}.cshtml", "/Areas/View/Views/Shared/{0}.cshtml" };
            return locations.Union(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["template"] = context.ActionContext.RouteData.Values["Template"]?.ToString() ?? "Default";
        }
    }
}