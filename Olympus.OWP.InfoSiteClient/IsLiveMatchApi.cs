namespace Olympus.OWP.InfoSiteClient
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Olympus.OWP.InfoSiteClient.Models;

    public interface IsLiveMatchApi
    {
        Task<IsLiveSynchronizedResult> SetLiveMatches(IEnumerable<(int, int?)> liveMatches);

        Task<IsLiveSynchronizedResult> SetNonLiveMatches(IEnumerable<(int, int?)> nonLiveMatches);
    }

    public class IsLiveMatchApiImp : IsLiveMatchApi
    {
        private static string InfoSiteAPI = ConfigurationManager.AppSettings["InfositeAPI"] ?? string.Empty;

        private static Regex regex = new Regex(@"^(\d+)\:.+$");

        private const string InvalidChildMatch = "FAILED_UPDATE_CHILD_MATCH";

        public async Task<IsLiveSynchronizedResult> SetLiveMatches(IEnumerable<(int, int?)> liveMatches)
        {
            return await SyncMatches(liveMatches, true);
        }

        public async Task<IsLiveSynchronizedResult> SetNonLiveMatches(IEnumerable<(int, int?)> nonLiveMatches)
        {
            return await SyncMatches(nonLiveMatches, false);
        }

        private async Task<IsLiveSynchronizedResult> SyncMatches(IEnumerable<(int, int?)> matches, bool isLive)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(30);
                httpClient.BaseAddress = new Uri(InfoSiteAPI);
                var path = "api/EventSchedule/setEventsLive";

                var model = GetKeyValuePairOfMatch(matches, isLive);
                var content = new StringContent(model, Encoding.UTF8, "application/json");

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

        private string GetKeyValuePairOfMatch(IEnumerable<(int, int?)> matches, bool isLive)
        {
            var matchList = matches.GroupBy(x => x.Item1, x => x.Item2, (key, g) => (key, g.ToList()));
            var syncMatches = new List<LiveConfig>();

            foreach (var match in matchList)
            {
                (int mainMatchId, List<int?> subType) = match;

                if (subType.Any(x => !x.HasValue))
                {
                    syncMatches.Add(new LiveConfig
                    {
                        MatchId = mainMatchId,
                        IsLive = isLive,
                        Subtypes = new List<int>()
                    });
                }

                if (subType.Any(x => x.HasValue))
                {
                    syncMatches.Add(new LiveConfig
                    {
                        MatchId = mainMatchId,
                        IsLive = isLive,
                        Subtypes = subType.Where(y => y.HasValue).Select(y => y.Value).ToList()
                    });
                }
            }

            var result = new SyncIsLiveModel { Matches = syncMatches };

            return JsonConvert.SerializeObject(result); ;
        }

        private List<(int, int?)> GetFailedMatches(string synchronizedResult)
        {
            var failedMessages = JsonConvert.DeserializeObject<List<string>>(synchronizedResult);
            var matches = failedMessages.Where(l => regex.IsMatch(l))
                .Select(x => GetFailedMatch(x))
                .ToList();

            return matches;
        }

        private static (int, int?) GetFailedMatch(string synchronizedResult)
        {
            var elements = synchronizedResult.Split(':');

            if (elements[1].Equals(InvalidChildMatch))
            {
                return (int.Parse(elements[0]), ParseSubtype(elements[2]));
            }

            return (int.Parse(elements[0]), null);
        }

        private static int? ParseSubtype(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                var subtypeText = "subType ";
                var fromText = " from";
                var pFrom = result.IndexOf(subtypeText) + subtypeText.Length;
                var pTo = result.IndexOf(fromText);
                var subType = result.Substring(pFrom, pTo - pFrom);

                if (!string.IsNullOrWhiteSpace(subType))
                {
                    return int.Parse(result.Substring(pFrom, pTo - pFrom));
                }
            }

            return null;
        }
    }
}