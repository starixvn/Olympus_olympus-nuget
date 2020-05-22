using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Fanex.Logging;

namespace Olympus.HttpLogger
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private readonly bool logResponse;

        private readonly bool logJsonResponseOnly;

        private readonly List<string> excludePatterns;

        private readonly List<string> includePatterns;

        public LoggingFilterAttribute(bool logResponse, bool logJsonResponseOnly)
        {
            this.logResponse = logResponse;
            this.logJsonResponseOnly = logJsonResponseOnly;
            this.includePatterns = new List<string> { ".*" };
            this.excludePatterns = new List<string>();
        }

        public LoggingFilterAttribute(bool logResponse, bool logJsonResponseOnly, List<string> excludePatterns)
        {
            this.logResponse = logResponse;
            this.logJsonResponseOnly = logJsonResponseOnly;
            this.includePatterns = new List<string> { ".*" };
            this.excludePatterns = excludePatterns ?? new List<string>();
        }

        public LoggingFilterAttribute(bool logResponse, bool logJsonResponseOnly, List<string> includePatterns, List<string> excludePatterns)
        {
            this.logResponse = logResponse;
            this.logJsonResponseOnly = logJsonResponseOnly;
            this.includePatterns = includePatterns ?? new List<string>();
            this.excludePatterns = excludePatterns ?? new List<string>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.RequestContext.RouteData.Values.ContainsKey("OnActionExecuting-StartTime"))
            {
                filterContext.RequestContext.RouteData.Values.Add("OnActionExecuting-StartTime", DateTime.UtcNow);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var startTime = (DateTime)filterContext.RequestContext.RouteData.Values["OnActionExecuting-StartTime"];
            var executionTime = (DateTime.UtcNow - startTime).TotalMilliseconds;

            var context = filterContext.HttpContext;
            var request = context.Request;
            var response = context.Response;

            var username = context?.User?.Identity?.Name;
            var requestBody = ReadRequestBody(request);

            if (!IsInclude(request.Url.ToString()) || IsExcluded(request.Url.ToString()))
            {
                return;
            }

            if (response.Filter != null)
            {
                response.Filter = new CaptureMvcResponse(request.Url.PathAndQuery, request.Url.ToString(), request.QueryString.ToString(), username, request.UserAgent, requestBody, logResponse, logJsonResponseOnly, executionTime, response.Filter);
            }
            else
            {
                WriteLog(request.Url.PathAndQuery, request.Url.ToString(), request.QueryString.ToString(), username, request.UserAgent, requestBody, executionTime);
            }
        }



        private static string ReadRequestBody(HttpRequestBase request)
        {
            try
            {
                request.InputStream.Seek(0, SeekOrigin.Begin);
                using (var streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    var text = streamReader.ReadToEnd();
                    request.InputStream.Seek(0, SeekOrigin.Begin);

                    return RegexUtility.ReplaceSensitiveText(text);
                }
            }
            catch (Exception e)
            {
                return $"Cannot read request body due to exception: {e.Message}";
            }

        }

        private void WriteLog(string pathAndQuery, string url, string queryString, string username, string userAgent, string requestBody, double executionTime)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{pathAndQuery}");
            stringBuilder.AppendLine($"Url: {url}");
            stringBuilder.AppendLine($"QueryString: {queryString}");
            stringBuilder.AppendLine($"Username: {username}");
            stringBuilder.AppendLine($"Server Time: {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}");
            stringBuilder.AppendLine($"Execution Time: {executionTime}");
            stringBuilder.AppendLine($"RequestBody: {requestBody}");
            stringBuilder.AppendLine("==================Response==================");
            stringBuilder.AppendLine("N/A due to response.Filter = null. Filtering is not allowed.");


            stringBuilder.AppendLine($"UserAgent: {userAgent}");
            stringBuilder.AppendLine($"GUID: {Guid.NewGuid()}");

            var logText = stringBuilder.ToString();
            Logger.Log.InfoAsync(logText);
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
