namespace TestWeb
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{action}");

            configuration.Filters.Add(new AuthorizeAttribute());
        }
    }
}
