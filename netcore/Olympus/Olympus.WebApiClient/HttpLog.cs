namespace Olympus.WebApiClient;

public class HttpLog
{
    public HttpLog(string requestUri, object requestData)
    {
        RequestUri = requestUri;
        RequestData = requestData;
    }

    public string RequestUri { get; set; }

    public object RequestData { get; set; }

    public object ResponseData { get; set; }
}
