// -----------------------------------------------------------------------
// <copyright file="GridDataRow.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;
    using Fanex.Excel.Enums;
    using Fanex.Excel.Extensions;
    using Newtonsoft.Json;

    /// <summary>
    /// Grid Data Row.
    /// </summary>
    [Serializable]
    public class GridDataRow : IGridDataRow
    {
        private string _cssClass;

        private int _currentColumnsIndex;

        public GridDataRow()
        {
            this.Columns = new List<Columns>();
            this.CssClass = string.Empty;
            this.SubRows = new List<GridDataRow>();
            this.RowType = Enums.RowType.Normal;
            this.FilterIndex = new Dictionary<string, object>();
            this._cssClass = string.Empty;
            this.IsEven = false;
        }

        [JsonIgnore]
        public GridOptions GridOptions { get; set; }

        [JsonIgnore]
        public GridDataRow ParentRow { get; set; }

        [JsonIgnore]
        public Dictionary<string, object> FilterIndex { get; set; }

        public List<Columns> Columns { get; set; }

        public string CssClass
        {
            get
            {
                if (this.RowType == Enums.RowType.SubTotal)
                {
                    this._cssClass += " subTotal";
                }

                this._cssClass = this._cssClass.Trim();
                return this._cssClass;
            }

            set
            {
                this._cssClass = value;
            }
        }

        public string CustomAttr { get; set; }

        [JsonIgnore]
        public RowType RowType { get; set; }

        [JsonIgnore]
        public bool IsEven { get; set; }

        public List<GridDataRow> SubRows { get; set; }

        /// <summary>
        /// Creates the columns.
        /// </summary>
        /// <param name="column">The column.</param>
        public void CreateColumns(Columns column)
        {
            column.GridDataRow = this;
            column.ColumnIndex = this._currentColumnsIndex;
            if (column.MergeColNumber > 1)
            {
                this._currentColumnsIndex += column.MergeColNumber - 1;
            }

            this._currentColumnsIndex++;
            this.Columns.Add(column);
        }

        public void CreateSubRows(GridDataRow subRow)
        {
            subRow.ParentRow = this;
            this.SubRows.Add(subRow);
        }

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        /// <exception cref="System.InvalidCastException">The source and the adding row must have same structure.</exception>
        public void AddValue(GridDataRow dataRow)
        {
            try
            {
                for (int i = 0; i < dataRow.Columns.Count; i++)
                {
                    var type = this.Columns[i].Value.GetType();
                    var refCellType = dataRow.Columns[i].Value.GetType();

                    if (type.Equals(refCellType))
                    {
                        if (Extensions.IsInteger(type))
                        {
                            this.Columns[i].Value = Convert.ToInt32(this.Columns[i].Value) + Convert.ToInt32(dataRow.Columns[i].Value);
                        }
                        else if (Extensions.IsDecimal(type))
                        {
                            this.Columns[i].Value = Convert.ToDouble(this.Columns[i].Value) + Convert.ToDouble(dataRow.Columns[i].Value);
                        }
                    }
                }
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("The source and the adding row must have same structure.");
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param name="isCloneSubRows">Is clone sub rows.</param>
        /// <returns>Grid Data Row.</returns>
        public GridDataRow Clone(bool isCloneSubRows = false)
        {
            var result = new GridDataRow();
            result.CssClass = this.CssClass;
            result.CustomAttr = this.CustomAttr;
            result.GridOptions = this.GridOptions;
            result.ParentRow = this.ParentRow;
            foreach (var item in this.FilterIndex)
            {
                result.FilterIndex.Add(item.Key, item.Value);
            }

            foreach (Columns column in Columns)
            {
                result.Columns.Add(column.Clone());
            }

            if (isCloneSubRows)
            {
                if (this.SubRows != null && this.SubRows.Count > 0)
                {
                    foreach (GridDataRow subRow in this.SubRows)
                    {
                        result.SubRows.Add(subRow.Clone());
                    }
                }
            }

            return result;
        }

        public bool ShouldSerializeSubRows()
        {
            return this.SubRows.Count > 0;
        }

        public bool IsParentRow()
        {
            return this.SubRows.Count > 0;
        }

        public int ParentLevel()
        {
            return this.ParentRow == null ? 1 : this.ParentRow.ParentLevel() + 1;
        }
    }
}
