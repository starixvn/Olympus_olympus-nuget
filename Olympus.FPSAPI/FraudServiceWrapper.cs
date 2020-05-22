using Olympus.FPSAPI.Client.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Olympus.FPSAPI.Client
{
    public interface FraudServiceWrapper
    {
        Task<List<CustomerDevice>> GetAssociatedCustomers(string host, int custId);
        Task<string> GetLatestIP(string host, int custId);
    }

    public class FraudServiceWrapperImp : FraudServiceWrapper
    {
        public async Task<List<CustomerDevice>> GetAssociatedCustomers(string host, int custId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var path = $"{host}/associatedcustomers/{custId}";
                var response = await client.GetAsync(path).ConfigureAwait(false);
                return response.IsSuccessStatusCode
                    ? await response.Content.ReadAsAsync<List<CustomerDevice>>().ConfigureAwait(false)
                    : new List<CustomerDevice>();
            }
        }

        public async Task<string> GetLatestIP(string host, int custId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var path = $"{host}/Get10LatestIps/{custId}";
                var response = await client.GetAsync(path).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                var result = await response.Content.ReadAsAsync<List<string>>().ConfigureAwait(false);

                return result.FirstOrDefault(ip => ip != "127.0.0.1");
            }
        }
    }
}
