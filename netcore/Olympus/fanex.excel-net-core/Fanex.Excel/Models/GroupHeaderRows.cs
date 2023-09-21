
namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [Serializable]
    public class GroupHeaderRows
    {
        public GroupHeaderRows()
        {
            this.Columns = new List<HeaderColumns>();
        }

        [JsonIgnore]
        public GridOptions GridOptions { get; set; }

        public List<HeaderColumns> Columns { get; set; }

        public void CreateGroupHeaderColumn(HeaderColumns groupHeaderColumn)
        {
            groupHeaderColumn.GridOptions = this.GridOptions;
            groupHeaderColumn.GroupHeaderRow = this;
            groupHeaderColumn.Width = 0;
            this.Columns.Add(groupHeaderColumn);
        }
    }
}
