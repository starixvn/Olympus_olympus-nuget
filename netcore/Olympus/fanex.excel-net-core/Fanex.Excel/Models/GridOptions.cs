// -----------------------------------------------------------------------
// <copyright file="GridOptions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Excel.Constants;
    using Fanex.Excel.Enums;
    using Newtonsoft.Json;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;    /// <summary>
                                /// TODO: Update summary.
                                /// </summary>
    [Serializable]
    public class GridOptions
    {
        public GridOptions()
        {
            this.GroupHeaderRows = new List<GroupHeaderRows>();
            this.Columns = new List<HeaderColumns>();
            this.BodyRows = new List<GridDataRow>();
            this.FooterRows = new List<GridDataRow>();
            this.ShowDistinctRow = true;
            this.ShowHighlightRow = true;
            this.TitleSeperatorRowNum = 1;
            this.NoDataText = ExcelConstants.ThereIsNoData;
            this.ShowPager = false;
            this.CustomPager = false;
            this.ShowFooterLineBreak = true;
            this.HiddenColumns = new List<int>();
            this.HeaderColumnHeight = 25;
            this.BodyRowHeight = 21;
            this.BodyRowHeightAutofit = false;
            this.FreezeHeader = false;
        }

        [JsonProperty("groupHeaderRows")]
        public List<GroupHeaderRows> GroupHeaderRows { get; set; }

        [JsonProperty("columns")]
        public List<HeaderColumns> Columns { get; set; }

        [JsonProperty("bodyRows")]
        public List<GridDataRow> BodyRows { get; set; }

        [JsonProperty("footerRows")]
        public List<GridDataRow> FooterRows { get; set; }

        [JsonProperty("showTitle")]
        public bool ShowTitle { get; set; }

        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }

        public string TitleInfoText { get; set; }

        [JsonIgnore]
        public string SubtitleInfoText { get; set; }

        [JsonIgnore]
        public int SubtitleRowNum { get; set; }

        [JsonProperty("showFooter")]
        public bool ShowFooter { get; set; }

        [JsonProperty("noDataText")]
        public string NoDataText { get; set; }

        [JsonProperty("showDistinctRow")]
        public bool ShowDistinctRow { get; set; }

        [JsonProperty("showHighlightRow")]
        public bool ShowHighlightRow { get; set; }

        [JsonProperty("customizeHeader")]
        public bool CustomizeHeader { get; set; }

        [JsonProperty("numberOfFrozenColumn")]
        public int NumberOfFrozenColumn { get; set; }

        [JsonIgnore]
        public bool FreezeHeader { get; set; }

        [JsonIgnore]
        public TitleType TitleType { get; set; }

        [JsonProperty("pagerOption")]
        public PagerOption PagerOption { get; set; }

        [JsonProperty("showPager")]
        public bool ShowPager { get; set; }

        [JsonProperty("showTree")]
        public bool ShowTree { get; set; }

        [JsonProperty("customPager")]
        public bool CustomPager { get; set; }

        [JsonIgnore]
        public int TitleSeperatorRowNum { get; set; }

        [JsonIgnore]
        public bool ShowFooterLineBreak { get; set; }

        [JsonProperty("hiddenColumns")]
        public List<int> HiddenColumns { get; set; }

        [JsonProperty("headerColumnHeight")]
        public int HeaderColumnHeight { get; set; }

        [JsonIgnore]
        public int HeaderColumnHeightExcel 
        { 
            get 
            { 
                return (int)(this.HeaderColumnHeight * 13.5); 
            } 
        }

        [JsonProperty("bodyRowHeight")]
        public int BodyRowHeight { get; set; }

        [JsonIgnore]
        public bool BodyRowHeightAutofit { get; set; }

        [JsonIgnore]
        public int BodyRowHeightExcel 
        { 
            get 
            { 
                return (int)(this.BodyRowHeight * 13.5); 
            } 
        }

        [JsonIgnore]
        public List<CustomExcelCellStyle> CustomExcelCellStyles { get; set; }

        [JsonIgnore]
        internal List<ICellStyle> CustomExcelHSSFCellStyles { get; set; }

        public bool ShouldSerializePagerOption()
        {
            return this.ShowPager && this.CustomPager;
        }

        public bool ShouldSerializeGroupHeaderRows()
        {
            return this.CustomizeHeader;
        }

        public bool ShouldSerializeHiddenColumns()
        {
            return this.HiddenColumns != null && this.HiddenColumns.Count > 0;
        }

        public string GetCellAlign(int cellIndex)
        {
            return this.Columns[cellIndex].CellAlign;
        }

        public void CreateGroupHeaderRow(GroupHeaderRows groupHeaderRow)
        {
            groupHeaderRow.GridOptions = this;
            this.GroupHeaderRows.Add(groupHeaderRow);
        }

        public void CreateColumns(HeaderColumns headerColumn)
        {
            headerColumn.GridOptions = this;
            this.Columns.Add(headerColumn);
        }

        public void CreateBodyRow(GridDataRow bodyRow)
        {
            bodyRow.GridOptions = this;
            this.BodyRows.Add(bodyRow);
        }

        public void CreateFooterRow(GridDataRow footerRow)
        {
            footerRow.GridOptions = this;
            this.FooterRows.Add(footerRow);
        }

        /// <summary>
        /// Clones the specified is clone body.
        /// </summary>
        /// <param name="isCloneBody">If set to <c>true</c> [is clone body].</param>
        /// <returns>Grid Options object.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. This is ok")]
        public GridOptions Clone(bool isCloneBody = false)
        {
            GridOptions clone = new GridOptions();
            clone.ShowTitle = this.ShowTitle;
            clone.TotalItems = this.TotalItems;
            clone.TitleInfoText = this.TitleInfoText;
            clone.ShowFooter = this.ShowFooter;
            clone.NoDataText = this.NoDataText;
            clone.ShowDistinctRow = this.ShowDistinctRow;
            clone.ShowHighlightRow = this.ShowHighlightRow;
            clone.CustomizeHeader = this.CustomizeHeader;
            clone.NumberOfFrozenColumn = this.NumberOfFrozenColumn;
            clone.TitleType = this.TitleType;
            clone.PagerOption = this.PagerOption == null ? null : this.PagerOption.Clone();
            clone.ShowPager = this.ShowPager;
            clone.CustomPager = this.CustomPager;
            clone.TitleSeperatorRowNum = this.TitleSeperatorRowNum;
            clone.BodyRowHeight = this.BodyRowHeight;
            clone.HeaderColumnHeight = this.HeaderColumnHeight;
            clone.BodyRowHeightAutofit = this.BodyRowHeightAutofit;
            int i = 0;
            int groupHeaderRowsCount = this.GroupHeaderRows == null ? 0 : this.GroupHeaderRows.Count;
            int columnsCount = this.Columns == null ? 0 : this.Columns.Count;
            int bodyRowsCount = this.BodyRows == null ? 0 : this.BodyRows.Count;
            int footerRowsCount = this.FooterRows == null ? 0 : this.FooterRows.Count;
            int hiddenColumns = this.HiddenColumns == null ? 0 : this.HiddenColumns.Count;
            for (; i < groupHeaderRowsCount; i++)
            {
                clone.GroupHeaderRows.Add(this.GroupHeaderRows[i]);
            }

            for (i = 0; i < columnsCount; i++)
            {
                clone.Columns.Add(this.Columns[i]);
            }

            if (isCloneBody)
            {
                for (i = 0; i < bodyRowsCount; i++)
                {
                    clone.BodyRows.Add(this.BodyRows[i]);
                }
            }

            for (i = 0; i < footerRowsCount; i++)
            {
                clone.FooterRows.Add(this.FooterRows[i]);
            }

            for (i = 0; i < hiddenColumns; i++)
            {
                clone.HiddenColumns.Add(this.HiddenColumns[i]);
            }

            return clone;
        }

        /// <summary>
        /// Gets the name of the header column by.
        /// </summary>
        /// <param name="headerName">Name of the header.</param>
        /// <returns>Header Column.</returns>
        public HeaderColumns GetHeaderColumnByName(string headerName)
        {
            foreach (var header in this.Columns)
            {
                if (header.Name.Equals(headerName))
                {
                    return header;
                }
            }

            return null;
        }
    }
}
