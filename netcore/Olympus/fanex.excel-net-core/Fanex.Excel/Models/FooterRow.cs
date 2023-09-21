
namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class FooterRow
    {
        public FooterRow()
        {
            this.Columns = new List<Columns>();
        }

        public List<Columns> Columns { get; set; }
    }
}
