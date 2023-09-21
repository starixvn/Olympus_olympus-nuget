using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Olympus.WebApiClient;

public interface RichHttpClient
{
    Task<T> Get<T>(string requestUri) where T : class;

    Task<T> Post<T>(string requestUri, object postData) where T : class;

    Task<T> PostJson<T>(string requestUri, object postData) where T : class;

    Task Post(string requestUri, object postData);

    Task<Stream> Download(string requestUri);
}

public class RichHttpClientImp : RichHttpClient
{
    private static readonly HttpClient JsonHttpClient = CreateJsonHttpClient();

    private static readonly HttpClient OctetStreamHttpClient = CreateOctetStreamHttpClient();

    private static HttpClient CreateJsonHttpClient()
    {
        return CreateHttpClient("application/json");
    }

    private static HttpClient CreateOctetStreamHttpClient()
    {
        return CreateHttpClient("application/octet-stream");
    }

    private static HttpClient CreateHttpClient(string contentType)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET HttpClient");

        return client;
    }

    public async Task<T> Get<T>(string requestUri) where T : class
    {
        var httpLog = new HttpLog(requestUri, string.Empty);
        using var response = await JsonHttpClient.GetAsync(requestUri).ConfigureAwait(false);
        return await TryReadObjectFromResponse<T>(response, httpLog).ConfigureAwait(false);
    }

    public async Task<T> Post<T>(string requestUri, object postData) where T : class
    {
        var httpLog = new HttpLog(requestUri, postData);
        var jsonRequest = JsonConvert.SerializeObject(postData);
        using var stringContent = new StringContent(jsonRequest);
        using var response = await JsonHttpClient.PostAsync(requestUri, stringContent).ConfigureAwait(false);
        return await TryReadObjectFromResponse<T>(response, httpLog).ConfigureAwait(false);
    }

    public async Task<T> PostJson<T>(string requestUri, object postData) where T : class
    {
        var jsonRequest = JsonConvert.SerializeObject(postData);
        var httpLog = new HttpLog(requestUri, jsonRequest);
        using var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        using var response = await JsonHttpClient.PostAsync(requestUri, stringContent).ConfigureAwait(false);
        return await TryReadObjectFromResponse<T>(response, httpLog).ConfigureAwait(false);
    }

    private static async Task<T> TryReadObjectFromResponse<T>(HttpResponseMessage response, HttpLog httpLog)
    {
        var json = string.Empty;
        try
        {
            json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (JsonReaderException jsonReaderException)
        {
            httpLog.ResponseData = json;

            var logDataMessage = JsonConvert.SerializeObject(httpLog);
            throw new JsonReaderException(logDataMessage, jsonReaderException);
        }
        catch (Exception exception)
        {
            var logDataMessage = JsonConvert.SerializeObject(httpLog);
            throw new Exception(logDataMessage, exception);
        }
    }

    public async Task Post(string requestUri, object postData)
    {
        var jsonRequest = JsonConvert.SerializeObject(postData);
        using var stringContent = new StringContent(jsonRequest);
        await JsonHttpClient.PostAsync(requestUri, stringContent).ConfigureAwait(false);
    }

    public async Task<Stream> Download(string requestUri)
    {
        return await OctetStreamHttpClient.GetStreamAsync(requestUri).ConfigureAwait(false);
    }
}