using System.Web.Mvc;
using Olympus.HttpLogger;
using ONEbook.UM.Filters;


namespace TestWeb.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CheckUMFilter());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new LoggingFilterAttribute(true, false));
        }
    }
}