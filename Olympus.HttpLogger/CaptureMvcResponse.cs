using System;
using System.IO;
using System.Text;
using Fanex.Logging;

namespace Olympus.HttpLogger
{
    public class CaptureMvcResponse : MemoryStream
    {
        private readonly Stream responseStream;

        private readonly MemoryStream copiedResponseStream;

        private readonly string pathAndQuery;
        private readonly string url;
        private readonly string queryString;
        private readonly string requestBody;
        private readonly string userAgent;
        private readonly string username;
        private readonly bool logResponse;
        private readonly bool logJsonResponseOnly;
        private readonly double executionTime;

        public CaptureMvcResponse(string pathAndQuery, string url, string queryString, string username, string userAgent, string requestBody, bool logResponse, bool logJsonResponseOnly, double executionTime, Stream responseStream)
        {
            this.pathAndQuery = pathAndQuery;
            this.url = url;
            this.queryString = queryString;
            this.username = username;
            this.userAgent = userAgent;
            this.requestBody = requestBody;
            this.responseStream = responseStream;
            this.logResponse = logResponse;
            this.logJsonResponseOnly = logJsonResponseOnly;
            this.executionTime = executionTime;
            copiedResponseStream = new MemoryStream();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            copiedResponseStream.Write(buffer, offset, count);
            responseStream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            WriteLog();
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            copiedResponseStream.Dispose();
            base.Dispose(disposing);
        }

        private void WriteLog()
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
            var responseText = GetResponseBody();
            var isJsonResponse = IsJson(responseText);


            if (logResponse && (!logJsonResponseOnly || isJsonResponse))
            {
                stringBuilder.AppendLine(GetResponseBody());
            }
            else
            {
                stringBuilder.AppendLine("Response log configuration is off");
            }

            stringBuilder.AppendLine($"UserAgent: {userAgent}");
            stringBuilder.AppendLine($"GUID: {Guid.NewGuid()}");

            var logText = stringBuilder.ToString();
            Logger.Log.InfoAsync(logText);
        }

        private string GetResponseBody()
        {
            try
            {
                copiedResponseStream.Seek(0, SeekOrigin.Begin);
                return Encoding.UTF8.GetString(copiedResponseStream.ToArray());
            }
            catch (Exception e)
            {
                return $"Exception: {e.Message} at GetResponseBody";
            }

        }

        public static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }
    }
}
