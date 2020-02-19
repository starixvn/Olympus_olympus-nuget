using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Fanex.Logging;

namespace Olympus.HttpLogger
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggingFilterApiAttribute : ActionFilterAttribute
    {
        private readonly bool logResponse;

        private readonly List<string> excludePatterns;

        private readonly List<string> includePatterns;

        public LoggingFilterApiAttribute(bool logResponse)
        {
            this.logResponse = logResponse;
            this.includePatterns = new List<string> { ".*" };
            this.excludePatterns = new List<string>();
        }

        public LoggingFilterApiAttribute(bool logResponse, List<string> excludePatterns)
        {
            this.logResponse = logResponse;
            this.includePatterns = new List<string> { ".*" };
            this.excludePatterns = excludePatterns ?? new List<string>();
        }

        public LoggingFilterApiAttribute(bool logResponse, List<string> includePatterns, List<string> excludePatterns)
        {
            this.logResponse = logResponse;
            this.includePatterns = includePatterns ?? new List<string>();
            this.excludePatterns = excludePatterns ?? new List<string>();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            if (!actionContext.ActionArguments.ContainsKey("OnActionExecuting-StartTime"))
            {
                actionContext.ActionArguments.Add("OnActionExecuting-StartTime", DateTime.UtcNow);
            }
        }


        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            var startTime = (DateTime)actionExecutedContext.ActionContext.ActionArguments["OnActionExecuting-StartTime"];
            var executionTime = (DateTime.UtcNow - startTime).TotalMilliseconds;

            var request = actionExecutedContext.Request;

            if (!IsInclude(request.RequestUri.ToString()) || IsExcluded(request.RequestUri.ToString()))
            {
                return;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{request.RequestUri.PathAndQuery}");
            stringBuilder.AppendLine($"Url: {request.RequestUri}");
            stringBuilder.AppendLine($"Username: {GetUsername()}");
            stringBuilder.AppendLine($"Server Time: {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}");
            stringBuilder.AppendLine($"Execution Time: {executionTime}");
            stringBuilder.AppendLine($"RequestBody: {ReadRequestBody(actionExecutedContext)}");
            stringBuilder.AppendLine("==================Response==================");

            if (logResponse)
            {
                stringBuilder.AppendLine(ReadResponseBody(actionExecutedContext));
            }
            else
            {
                stringBuilder.AppendLine("Response log configuration is off");
            }

            stringBuilder.AppendLine($"UserAgent: {request.Headers.UserAgent}");
            stringBuilder.AppendLine($"GUID: {Guid.NewGuid()}");

            var logText = stringBuilder.ToString();
            Logger.Log.InfoAsync(logText);
        }

        private static string ReadRequestBody(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    var context = (HttpContextBase)actionExecutedContext.Request.Properties["MS_HttpContext"];
                    context.Request.InputStream.Seek(0, SeekOrigin.Begin);
                    context.Request.InputStream.CopyTo(stream);
                    var requestBody = Encoding.UTF8.GetString(stream.ToArray());
                    requestBody = RegexUtility.ReplaceSensitiveText(requestBody);

                    return requestBody;
                }
            }
            catch (Exception e)
            {
                return $"Cannot read request body due to exception: {e.Message}";
            }
        }

        private static string ReadResponseBody(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                var response = actionExecutedContext.Response;
                using (var stream = new MemoryStream())
                {
                    response.Content.CopyToAsync(stream);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch (Exception e)
            {
                return $"Cannot read response body due to exception: {e.Message}";
            }

        }

        private static string GetUsername()
        {
            var username = HttpContext.Current?.User?.Identity?.Name;
            return string.IsNullOrEmpty(username) ? string.Empty : username;
        }

        private bool IsExcluded(string url)
        {
            foreach (var pattern in excludePatterns)
            {
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                if (regex.IsMatch(url))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInclude(string url)
        {
            foreach (var pattern in includePatterns)
            {
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                if (regex.IsMatch(url))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
