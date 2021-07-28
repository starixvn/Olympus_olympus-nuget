namespace Olympus.OWP.InfoSiteClient
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public interface IsLiveMatchApi
    {
        Task<IsLiveSynchronizedResult> SetLiveMatches(IEnumerable<int> liveMatches);

        Task<IsLiveSynchronizedResult> SetNonLiveMatches(IEnumerable<int> nonLiveMatches);
    }

    public class IsLiveMatchApiImp : IsLiveMatchApi
    {
        private static string InfoSiteAPI = ConfigurationManager.AppSettings["InfositeAPI"] ?? string.Empty;

        private static Regex regex = new Regex(@"^(\d+)\:.+$");

        public async Task<IsLiveSynchronizedResult> SetLiveMatches(IEnumerable<int> liveMatches)
        {
            return await SyncMatches(liveMatches, true);
        }

        public async Task<IsLiveSynchronizedResult> SetNonLiveMatches(IEnumerable<int> nonLiveMatches)
        {
            return await SyncMatches(nonLiveMatches, false);
        }

        private async Task<IsLiveSynchronizedResult> SyncMatches(IEnumerable<int> matches, bool isLive)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(30);
                httpClient.BaseAddress = new Uri(InfoSiteAPI);
                var path = "api/EventSchedule/setEventsLive";

                var keyValuePairs = GetKeyValuePairOfMatch(matches, isLive);
                var content = new FormUrlEncodedContent(keyValuePairs);

                var response = await httpClient.PostAsync(path, content);
                var responseText = await response.Content.ReadAsStringAsync();

                var synchronizedResult = response.IsSuccessStatusCode ? responseText : "[]";
                var errorMessage = response.IsSuccessStatusCode && responseText == "[]" ? string.Empty : responseText;
                var failedMatches = response.IsSuccessStatusCode ? GetFailedMatches(synchronizedResult) : matches.ToList();
                var successfulMatches = matches.Except(failedMatches).ToList();

                return new IsLiveSynchronizedResult
                {
                    FailedMatches = failedMatches,
                    SuccessfulMatches = successfulMatches,
                    ErrorMessage = errorMessage
                };
            }
        }

        private List<KeyValuePair<string, string>> GetKeyValuePairOfMatch(IEnumerable<int> matches, bool isLive)
        {
            var result = new List<KeyValuePair<string, string>>();
            var index = 0;
            foreach (var matchId in matches)
            {
                var matchIdPair = new KeyValuePair<string, string>($"live_configs[{index}][match_id]", matchId.ToString());
                var isLivePair = new KeyValuePair<string, string>($"live_configs[{index}][is_live]", isLive ? "true" : "false");
                result.Add(matchIdPair);
                result.Add(isLivePair);
                index++;
            }

            var unixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var timestampPair = new KeyValuePair<string, string>("_API_BEGIN_STAMP", unixTimestamp.ToString());
            result.Add(timestampPair);

            return result;
        }

        private List<int> GetFailedMatches(string synchronizedResult)
        {
            var failedMessages = JsonConvert.DeserializeObject<List<string>>(synchronizedResult);
            return failedMessages.Where(l => regex.IsMatch(l))
                .Select(l => regex.Match(l).Groups[1].Value)
                .Select(l => int.Parse(l))
                .ToList();
        }
    }
}
