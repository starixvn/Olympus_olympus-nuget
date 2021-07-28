using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Fanex.Logging;
using Newtonsoft.Json;
using Olympus.OWP.InfoSiteClient.Extensions;
using Olympus.OWP.InfoSiteClient.Models;

namespace Olympus.OWP.InfoSiteClient
{
    public class PullingChildMatch
    {
        private static string InfoSiteAPI = ConfigurationManager.AppSettings["InfositeAPI"] ?? string.Empty;

        public IList<ChildMatch> Query(DateTime from, DateTime to)
        {
            var stamp = DateTime.Today.AddYears(-10);
            var matches = new List<ChildMatch>();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(30);
                httpClient.BaseAddress = new Uri(InfoSiteAPI);
                var path = string.Format("api/EventSchedule/getChildEventsByDate?rd_checked=true&date_from={0:yyyy-MM-dd}&date_to={1:yyyy-MM-dd}&stamp={2:yyyy-MM-dd HH:mm:ss}", from, to, stamp);

                var response = httpClient.GetAsync(path).Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = response.Content.ReadAsStringAsync().Result.ToString();
                    matches = JsonConvert.DeserializeObject<List<ChildMatch>>(jsonData, new TrimmingConverter());
                }
                else
                {
                    var responseText = response.Content.ReadAsStringAsync().Result.ToString();
                    LogService.Error($"httpClient Error: {response.ReasonPhrase}. Response: {responseText}");
                }

                return matches;
            }
        }
    }
}
