namespace Olympus.OWP.InfoSiteClient.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SyncIsLiveModel
    {
        [JsonProperty(PropertyName = "live_configs")]
        public IList<LiveConfig> Matches { get; set; }
    }

    public class LiveConfig
    {
        [JsonProperty(PropertyName = "match_id")]
        public int MatchId { get; set; }

        [JsonProperty(PropertyName = "is_live")]
        public bool IsLive { get; set; }

        [JsonProperty(PropertyName = "subtype_list")]
        public IList<int> Subtypes { get; set; }
    }
}
