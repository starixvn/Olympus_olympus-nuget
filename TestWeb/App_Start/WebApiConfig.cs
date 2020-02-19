using System.Web.Http;

namespace TestWeb.App_Start
{
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