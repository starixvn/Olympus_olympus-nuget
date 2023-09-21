// -----------------------------------------------------------------------
// <copyright file="PagerOption.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Pager Option.
    /// </summary>
    [Serializable]
    public class PagerOption
    {
        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("totalItem")]
        public int TotalItem { get; set; }

        [JsonProperty("itemsPerPageArray")]
        public List<int> ItemsPerPageArray { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Pager Option object.</returns>
        public PagerOption Clone()
        {
            PagerOption clone = new PagerOption();
            clone.ItemsPerPage = this.ItemsPerPage;
            clone.CurrentPage = this.CurrentPage;
            clone.TotalItem = this.TotalItem;
            clone.ItemsPerPageArray = new List<int>();
            int itemsPerPageArrayCount = this.ItemsPerPageArray.Count;
            for (int i = 0; i < itemsPerPageArrayCount; i++)
            {
                clone.ItemsPerPageArray.Add(this.ItemsPerPageArray[i]);
            }

            return clone;
        }
    }
}
