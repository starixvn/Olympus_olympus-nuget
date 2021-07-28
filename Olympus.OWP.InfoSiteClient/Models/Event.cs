using System;
using System.Xml.Serialization;
using Fanex.Logging;
using Olympus.OWP.InfoSiteClient.Extensions.XMLExtensions;

namespace Olympus.OWP.InfoSiteClient.Models
{
    [Serializable]
    [XMLName("Row")]
    public class Event
    {
        private readonly static DateTime MinDate = new DateTime(2000, 1, 1);

        public Event(CommonMatchEx infositeMatch)
        {
            var extInfo = infositeMatch.ext_info ?? new SoccerEventSchedule();
            InfoSiteMatchKey = $"{infositeMatch.match_id}-{infositeMatch.website_id}-{infositeMatch.is_import}";
            InfoSiteMergeMatchKey = GetInfoSiteMergeMatchKey(infositeMatch);
            MatchId = GetMatchId(infositeMatch);
            LeagueName = infositeMatch.league_name;
            HomeName = GetHomeName(infositeMatch, extInfo);
            AwayName = GetAwayName(infositeMatch, extInfo);
            WatchBet = ConvertNullToEmpty(extInfo.wb);
            Round = ConvertNullToEmpty(extInfo.round);
            OutSide = ConvertNullToEmpty(extInfo.code);
            Agent = ConvertNullToEmpty(extInfo.agent);
            ExtraTimeRemark = ConvertNullToEmpty(extInfo.et_pk);
            Venue = ConvertNullToEmpty(extInfo.venue);
            LocalTV = ConvertNullToEmpty(extInfo.local_tv);
            LiveTV = ConvertNullToEmpty(extInfo.tv_live);
            Website = ConvertNullToEmpty(extInfo.website1);
            Note = GetNote(extInfo);
            Provider = GetProvider(infositeMatch.ext_info?.provider);
            UpdatedTime = GetUpdateTime(infositeMatch, extInfo);
            IsChecked = extInfo.rd_checked.HasValue && extInfo.rd_checked.Value;
            GameDate = GetGameDate(infositeMatch);
        }

        [XmlIgnore]
        public DateTime GameDate { get; set; }

        public bool IsChecked { get; set; }

        public string EventDate => GameDate.Date.ToString();

        public string KickOffTime => GameDate.ToString("hh:mm tt");

        public string InfoSiteMatchKey { get; set; }

        public string InfoSiteMergeMatchKey { get; set; }

        public int? MatchId { get; set; }

        public string LeagueName { get; set; }

        public string HomeName { get; set; }

        public string AwayName { get; set; }

        public string WatchBet { get; set; }

        public string Round { get; set; }

        public string OutSide { get; set; }

        public string Agent { get; set; }

        public string ExtraTimeRemark { get; set; }

        public string Venue { get; set; }

        public string LocalTV { get; set; }

        public string LiveTV { get; set; }

        public string Website { get; set; }

        public string Note { get; set; }

        public string Provider { get; set; }

        public DateTime UpdatedTime { get; set; }

        private static string GetHomeName(CommonMatchEx infositeMatch, SoccerEventSchedule extInfo)
        {
            var isNeutral = extInfo.is_neutral.HasValue && extInfo.is_neutral.Value;
            var isReverse = extInfo.is_team_reverse.HasValue && extInfo.is_team_reverse.Value;
            var homename = isReverse ? infositeMatch.away_name : infositeMatch.home_name;
            return isNeutral ? homename + " (N)" : homename;
        }

        private static string GetAwayName(CommonMatchEx infositeMatch, SoccerEventSchedule extInfo)
        {
            var isReverse = extInfo.is_team_reverse.HasValue && extInfo.is_team_reverse.Value;
            return isReverse ? infositeMatch.home_name : infositeMatch.away_name;
        }

        private string ConvertNullToEmpty(string value)
        {
            return string.IsNullOrEmpty(value)
                ? string.Empty
                : value.Trim();
        }

        private static string GetInfoSiteMergeMatchKey(CommonMatchEx infositeMatch)
        {
            var matchKey = $"{infositeMatch.match_id}-{infositeMatch.website_id}-{infositeMatch.is_import}";
            var mergeMatchKey = string.IsNullOrEmpty(infositeMatch.merge_match) ? "" : infositeMatch.merge_match;

            return mergeMatchKey.Replace(matchKey, "").Trim(';');
        }

        private static string GetNote(SoccerEventSchedule extInfo)
        {
            if (extInfo == null)
            {
                return string.Empty;
            }

            var remark2 = string.IsNullOrEmpty(extInfo.remark2) ? string.Empty : "@#" + extInfo.remark2;
            return extInfo.remark + remark2;
        }


        private DateTime GetGameDate(CommonMatchEx infositeMatch)
        {
            var exInfoGameDate = infositeMatch.ext_info?.game_date != null && infositeMatch.ext_info.game_date.HasValue
                ? infositeMatch.ext_info.game_date.Value
                : MinDate;

            if (IsChecked && exInfoGameDate <= MinDate)
            {
                LogService.Warn($"{InfoSiteMatchKey} - has exInfoGameDate null");
            }

            return ConvertTimeToUTC8(exInfoGameDate);
        }

        private DateTime GetUpdateTime(CommonMatchEx infositeMatch, SoccerEventSchedule extInfo)
        {
            var exInfoTime = extInfo.update_time.HasValue ? extInfo.update_time.Value : MinDate;
            var matchTime = infositeMatch.update_timestamp.HasValue ? infositeMatch.update_timestamp.Value : MinDate;

            return exInfoTime > matchTime ? exInfoTime : matchTime;
        }

        private DateTime ConvertTimeToUTC8(DateTime date)
        {
            return date.AddHours(8);
        }

        private int? GetMatchId(CommonMatchEx infositeMatch)
        {
            int? matchId = null;
            if (infositeMatch.ext_info != null && infositeMatch.ext_info.create_match_id != 0)
            {
                matchId = infositeMatch.ext_info.create_match_id;
            }

            return matchId;
        }

        private string GetProvider(string providers)
        {
            return string.IsNullOrWhiteSpace(providers) ? string.Empty : providers.Replace("/", string.Empty);
        }
    }
}