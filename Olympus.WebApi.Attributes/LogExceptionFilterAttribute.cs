using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Fanex.Logging.Extensions.Web;

namespace Olympus.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ExceptionHandler.Handle(actionExecutedContext.Exception);
        }

        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                await ExceptionHandler.HandleAsync(actionExecutedContext.Exception);
            }
        }
    }
}