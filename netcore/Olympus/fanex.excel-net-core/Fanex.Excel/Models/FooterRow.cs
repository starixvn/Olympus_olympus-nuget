// -----------------------------------------------------------------------
// <copyright file="FooterRow.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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
