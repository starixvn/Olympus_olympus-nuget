namespace TestWeb
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Fanex.Data;
    using Fanex.Data.SqlServer;
    using Fanex.Logging;
    using Fanex.Logging.Sentry.RavenClient;
    using SharpRaven;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            var logEngine = new List<ILogEngine>();
            var sentryEngineOptions = new SentryEngineOptions
            {
                Dsn = new Dsn(ConfigurationManager.AppSettings["SentryToken"]),
                WriteLogTimeoutSeconds = 1
            };

            logEngine.Add(new SentryLogging(sentryEngineOptions));
            LogManager.SetDefaultLogCategory("TestWeb").Use(logEngine);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            DbSettingProviderManager
              .StartNewSession()
              .WithSqlServer()
              .UseDefaultDbSettingProvider(resourcePath: "~/App_Data")
              .UseDefaultConnectionStringProvider()
              .Run();
        }
    }
}
