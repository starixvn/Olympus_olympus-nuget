namespace TestWeb
{
    using System.Web.Mvc;
    using Olympus.HttpLogger;

    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
            filters.Add(new LoggingFilterAttribute(true, false));
        }
    }
}