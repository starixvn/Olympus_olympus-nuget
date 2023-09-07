namespace Olympus.WebApi.Attributes
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    actionContext.ModelState);
            }
        }
    }
}