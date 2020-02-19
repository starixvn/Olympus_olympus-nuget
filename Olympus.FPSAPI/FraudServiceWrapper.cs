using Olympus.FPSAPI.Client.Model;
using System;
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

        Task<List<CustomerDevice>> GetAssociatedCustomers(string host, string username, string siteName, bool isCreditSite);

        Task<string> GetLatestIP(string host, string username, string siteName, bool isCreditSite);
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

        public async Task<List<CustomerDevice>> GetAssociatedCustomers(string host, string username, string siteName, bool isCreditSite)
        {
            var realSiteName = GetSiteName(siteName, isCreditSite);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var path = $"{host}/associatedaccounts/{username}/{realSiteName}";
                var response = await client.GetAsync(path).ConfigureAwait(false);
                return response.IsSuccessStatusCode
                    ? await response.Content.ReadAsAsync<List<CustomerDevice>>().ConfigureAwait(false)
                    : new List<CustomerDevice>();
            }
        }

        public async Task<string> GetLatestIP(string host, string username, string siteName, bool isCreditSite)
        {
            var realSiteName = GetSiteName(siteName, isCreditSite);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var path = $"{host}/get10latestips/{username}/{realSiteName}";
                var response = await client.GetAsync(path).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                var result = await response.Content.ReadAsAsync<List<string>>().ConfigureAwait(false);

                return result.FirstOrDefault(ip => ip != "127.0.0.1");
            }
        }

        private static Dictionary<string, string> SiteMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {  "asiabet88", "fun88" },
            {  "mansion88", "m88" },
            {  "haifa", "bbin" },
            {  "cn88", "cn88" },
        };

        private string GetSiteName(string siteName, bool isCreditSite)
        {
            if (SiteMap.ContainsKey(siteName.ToLower()))
            {
                return SiteMap[siteName.ToLower()];
            }

            if (isCreditSite)
            {
                return siteName.Equals("789y", StringComparison.CurrentCultureIgnoreCase)
                    ? "maxbet789y"
                    : "maxbet";
            }

            return siteName;
        }
    }
}
