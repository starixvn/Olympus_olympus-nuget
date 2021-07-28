using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using Fanex.Logging;
using Newtonsoft.Json;
using Olympus.OWP.InfoSiteClient.Extensions;
using Olympus.OWP.InfoSiteClient.Models;

namespace Olympus.OWP.InfoSiteClient
{
    public class PullingMainMatch
    {
        private static string InfoSiteAPI = ConfigurationManager.AppSettings["InfositeAPI"] ?? string.Empty;

        public IList<CommonMatchEx> Query(DateTime from, DateTime to, DateTime stamp)
        {
            var matches = new List<CommonMatchEx>();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(30);
                httpClient.BaseAddress = new Uri(InfoSiteAPI);
                var path = string.Format("api/EventSchedule/getEventsByDate?rd_checked=true&date_from={0:yyyy-MM-dd}&date_to={1:yyyy-MM-dd}&stamp={2:yyyy-MM-dd HH:mm:ss}", from, to, stamp);

                var response = httpClient.GetAsync(path).Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = response.Content.ReadAsStringAsync().Result.ToString();
                    matches = JsonConvert.DeserializeObject<List<CommonMatchEx>>(jsonData, new TrimmingConverter());
                }
                else
                {
                    var responseText = response.Content.ReadAsStringAsync().Result.ToString();
                    LogService.Error($"httpClient Error: {response.ReasonPhrase}. Response: {responseText}");
                }

                return matches.Any() ? FilterMatches(matches) : matches;
            }
        }

        private List<CommonMatchEx> FilterMatches(IList<CommonMatchEx> matches)
        {
            var hasExInfoEvents = matches.Where(e => e.ext_info != null);
            var checkedMatches = hasExInfoEvents.Where(e => e.ext_info.rd_checked.GetValueOrDefault(false));
            var matchedMatches = hasExInfoEvents.Where(e => !e.ext_info.rd_checked.GetValueOrDefault(false) && e.ext_info.create_match_id > 0);
            var filteredMatches = checkedMatches.Union(matchedMatches);
            var soccerMatches = filteredMatches.Where(m => m.ext_info.sport_id == 1);

            return soccerMatches.ToList();
        }
    }
}
