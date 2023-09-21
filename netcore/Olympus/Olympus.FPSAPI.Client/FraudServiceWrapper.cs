using Newtonsoft.Json;
using Olympus.FPSAPI.Client.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Olympus.FPSAPI.Client;

public interface FraudServiceWrapper
{
    Task<List<CustomerDevice>> GetAssociatedCustomers(string host, int custId);

    Task<string> GetLatestIP(string host, int custId);

    Task<string> GetCategory(string host, string custId);

    Task<List<CustomerByUsername>> GetAssociatedCustomerByDeviceOrManual(string host, int custId, int associationType);

    Task<List<CustomerById>> GetAssociatedCustomersByIp(string host, int custId);
}

public class FraudServiceWrapperImp : FraudServiceWrapper
{
    private const int AssociatedByIP = 3;
    static readonly HttpClient jsonClient = new();
    static readonly HttpClient xmlClient = new();

    public async Task<List<CustomerDevice>> GetAssociatedCustomers(string host, int custId)
    {
        jsonClient.DefaultRequestHeaders.Accept.Clear();
        jsonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var path = $"{host}/associatedcustomers/{custId}";
        var response = await jsonClient.GetAsync(path).ConfigureAwait(false);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadAsAsync<List<CustomerDevice>>().ConfigureAwait(false)
            : new List<CustomerDevice>();
    }

    public async Task<string> GetLatestIP(string host, int custId)
    {
        jsonClient.DefaultRequestHeaders.Accept.Clear();
        jsonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var path = $"{host}/Get10LatestIps/{custId}";
        var response = await jsonClient.GetAsync(path).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            return string.Empty;
        }

        var result = await response.Content.ReadAsAsync<List<string>>().ConfigureAwait(false);

        return result.FirstOrDefault(ip => ip != "127.0.0.1");
    }

    public async Task<string> GetCategory(string host, string custId)
    {
        var result = new List<string>();

        xmlClient.DefaultRequestHeaders.Accept.Clear();
        xmlClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

        var path = $"{host}/GetCustomerClassification/?custIdList={custId}";
        var response = await xmlClient.GetAsync(path).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            return string.Empty;
        }

        var responseData = await response.Content.ReadAsAsync<string>();

        var xdoc = XDocument.Parse(responseData);
        var elements = xdoc.Descendants("Root").Descendants("CustomerClassification");
        if (elements.Any())
        {
            foreach (var element in elements)
            {
                result.Add(element.Attribute("CategoryName").Value);
            }
        }

        return string.Join(", ", result);
    }

    public async Task<List<CustomerByUsername>> GetAssociatedCustomerByDeviceOrManual(string host, int custId, int associationType)
    {
        jsonClient.DefaultRequestHeaders.Accept.Clear();
        jsonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var path = $"{host}/cts/GetAssociationPerType?custId={custId}&associationType={associationType}";
        var response = await jsonClient.GetAsync(path).ConfigureAwait(false);
        var responseData = response.IsSuccessStatusCode
            ? await response.Content.ReadAsAsync<string>().ConfigureAwait(false)
            : string.Empty;

        return JsonConvert.DeserializeObject<List<CustomerByUsername>>(responseData);
    }

    public async Task<List<CustomerById>> GetAssociatedCustomersByIp(string host, int custId)
    {
        jsonClient.DefaultRequestHeaders.Accept.Clear();
        jsonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var path = $"{host}/cts/GetAssociationPerType?custId={custId}&associationType={AssociatedByIP}";
        var response = await jsonClient.GetAsync(path).ConfigureAwait(false);
        var responseData = response.IsSuccessStatusCode
            ? await response.Content.ReadAsAsync<string>().ConfigureAwait(false)
            : string.Empty;

        return JsonConvert.DeserializeObject<List<CustomerById>>(responseData);
    }
}