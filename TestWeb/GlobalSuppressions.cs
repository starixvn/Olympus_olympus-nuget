
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Sonar Code Smell",
    "S2325:Methods and properties that don't access instance data should be static",
    Justification = "<Pending>",
    Scope = "member",
    Target = "~M:Fanex.TicketControl.Website.MvcApplication.Application_Start")]
[assembly: SuppressMessage(
    "Sonar Code Smell",
    "S3994:URI Parameters should not be strings",
    Justification = "<Pending>",
    Scope = "member",
    Target = "~M:Fanex.TicketControl.Website.Areas.UserLogin.Controllers.SsoController.AccountingLogin(System.String,System.String,System.String)" +
    "~System.Web.Mvc.ActionResult")]
