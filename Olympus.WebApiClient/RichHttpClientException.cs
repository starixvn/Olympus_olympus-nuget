using System;
using System.Net;

namespace Olympus.WebApiClient
{
    public class RichHttpClientException : Exception
    {
        public RichHttpClientException(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }

        public int StatusCode { get; set; }

        public override string Message
        {
            get
            {
                return StatusCode.ToString();
            }
        }
    }
}