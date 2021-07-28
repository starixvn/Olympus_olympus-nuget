using System;
using System.Collections.Generic;

namespace Olympus.OWP.InfoSiteClient.Models
{
    // Model updated by Joy.Chang at 2018/10/26

    public class CommonSubMatch
    {
        // id of match of the source provider
        public Int32 match_id { get; set; }
        // id of source provider
        public Nullable<Int32> website_id { get; set; }
        // id coverage of source provider
        public Nullable<Int32> coverage_id { get; set; }
        // is import match (other source should import as info site match then able to create admin site)
        public bool is_import { get; set; }
        // is virtual match (to distinguish soccerway or virtual match/other source)
        public bool is_virtual { get; set; }

        public int infosite_match_id { get; set; }
        // mbc match id if already create to admin site
        public Int32 create_match_id { get; set; }

        public Nullable<DateTime> game_date { get; set; }

        public String match_url { get; set; }
        // original match id before import (to look up the raw source match in imported match)
        public int ori_match_id { get; set; }
        // import match id after import (to look up the info site match in raw source match)
        public int imp_match_id { get; set; }

        public Nullable<bool> is_postponed { get; set; }
        // Null: Normal, 0: Normal, 1: Postponed, 2: Cancelled, 3: No kick off time
        public Nullable<int> match_status { get; set; }
    }

    public class CommonMatch : CommonSubMatch
    {
        #region league team id/name info
        public Nullable<Int32> ibc_league_id { get; set; }
        public Nullable<Int32> league_id { get; set; }
        public String league_name { get; set; }
        public Nullable<Int32> ibc_home_id { get; set; }
        public Nullable<Int32> home_id { get; set; }
        public String home_name { get; set; }
        public Nullable<Int32> ibc_away_id { get; set; }
        public Nullable<Int32> away_id { get; set; }
        public String away_name { get; set; }
        #endregion league team id/name info

        #region league team attribute info
        public bool is_league_black { get; set; }
        public bool is_home_black { get; set; }
        public bool is_away_black { get; set; }
        public bool is_league_ignore { get; set; }
        public bool is_home_ignore { get; set; }
        public bool is_away_ignore { get; set; }
        public bool is_league_bad_ranking { get; set; }
        public bool is_home_bad_ranking { get; set; }
        public bool is_away_bad_ranking { get; set; }
        public bool is_home_downgrade { get; set; }
        public bool is_away_downgrade { get; set; }
        public bool is_home_new { get; set; }
        public bool is_away_new { get; set; }
        #endregion league team attribute info

        #region system statistics info
        // Round number or tag
        public string round { get; set; }
        // Round total number or tag
        public string round_total { get; set; }

        public string league_level3_name { get; set; }
        // the last update stamp in cache
        public Nullable<DateTime> update_timestamp { get; set; }
        // merge matches: match_keys split with comma
        public string merge_match { get; set; }
        // Home round number.(InfoSite provide)
        public int home_game_no { get; set; }
        // Away round number.(InfoSite provide)
        public int away_game_no { get; set; }
        // Round total number.(InfoSite provide)
        public int team_matches { get; set; }
        // 0: cup, 1: regular, 2: final, 3: play off, 4: promotion relegation
        #endregion system statistics info

        #region other configs
        public int league_type { get; set; }
        // refer to mbc league_id
        public Int32 home_main_league_id { get; set; }
        // main league name
        public String home_main_league_name { get; set; }
        // refer to mbc league_id
        public Int32 away_main_league_id { get; set; }
        // main league name
        public String away_main_league_name { get; set; }

        public bool localize_checked { get; set; }
        #endregion other configs
    }

    public class CommonMatchEx : CommonMatch
    {
        // user config
        public SoccerEventSchedule ext_info { get; set; }
        // list of merged match
        public List<CommonSubMatch> ls_merge_match { get; set; }
    }

    public class ChildMatch
    {
        #region parent match info
        // The info site match ID of parent match.
        public Int64 match_id { get; set; }
        // The created ibc match ID of parent match corresponding to match_id.
        public Int64 ibc_match_id { get; set; }
        #endregion parent match info

        #region child match info
        // The child league ID.
        public Int32 league_id { get; set; }
        // The child league name.
        public String league_name { get; set; }
        // The child home team ID.
        public Int32 home_id { get; set; }
        // The child home team name.
        public String home_name { get; set; }
        // The child away team ID.
        public Int32 away_id { get; set; }
        // The child away team name.
        public String away_name { get; set; }

        // The ibc match ID of this child match.
        public Nullable<Int64> create_match_id { get; set; }
        // The match code of this child match.
        public String create_match_code { get; set; }
        // 0: live, 1: live+deadball, 2: hedge
        public String create_option;
        // Diff second
        // 0:-1
        // 1:-60
        // 2:-300
        // 3:1
        // 4:300
        // 5:180
        public Nullable<Int32> create_time_diff { get; set; }
        // Diff second
        public Nullable<Int32> create_time_diff_sec { get; set; }
        // 0: closed.
        // 1: running.
        // 2: postponed. 
        // 3: internal.
        // 4: completed.
        public Nullable<Int32> create_event_status { get; set; }
        // Is Live message.
        public string create_is_live_msg { get; set; }
        // Match datetime.
        public Nullable<DateTime> game_date { get; set; }
        // Is link parent to child. if set true, this use for "child attribute follow parent when parent changed". BUT this function no implement due to requirement no need
        public Boolean is_link { get; set; }
        // 0:Child League, 1:ExtraTime, 2:Penalty Handicap 3:Penalty Over-Under
        public int league_type { get; set; }
        // Is delete.
        public Boolean is_delete { get; set; }
        // changed stamp
        public DateTime stamp { get; set; }
        #endregion child match info
    }
}
