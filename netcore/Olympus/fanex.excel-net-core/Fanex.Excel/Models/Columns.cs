// -----------------------------------------------------------------------
// <copyright file="Columns.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;
    using Fanex.Excel.Enums;
    using Newtonsoft.Json;
    using NPOI.SS.UserModel;

    /// <summary>
    /// The Columns.
    /// </summary>
    [Serializable]
    public class Columns : IColumns
    {
        private object _value;

        [JsonIgnore]
        private bool _bold;

        public Columns()
            : this(0)
        {

        }

        public Columns(int columnIndex = 0)
        {
            this.ColumnIndex = columnIndex;
            this._bold = false;
            this.CellAlignment = HAlignment.Null;
            this.DisplayValue = null;
            this.TDClass = string.Empty;
            this.StartIndex = new List<int>();
            this.EndIndex = new List<int>();
            this.TextFont = new List<ExcelFont>();
            this.CustomCellType = ExcelCellType.Null;
            this.WrapText = false;
            this.HiddenColumn = false;
            this.Indention = (short)0;
            this.CustomExcelCellStyleIndex = -1;
        }

        public Columns(int columnIndex, string name, object value, HAlignment cellAlignment = HAlignment.Right)
            : this(columnIndex)
        {
            this.Name = name;
            this.Value = value;
            this.CellAlignment = cellAlignment;
        }

        [JsonIgnore]
        public GridDataRow GridDataRow { get; set; }

        [JsonIgnore]
        public int ColumnIndex { get; set; }

        public string Name { get; set; }

        public object Value
        {
            get
            {
                return this._value;
            }

            set
            {
                if (value == null)
                {
                    this._value = null;
                }
                else
                {
                    if (!CheckValidValueType(value.GetType()))
                    {
                        throw new InvalidOperationException("\"" + value.GetType().FullName + "\" is not a valid NPOI CellType.");
                    }

                    if (value.GetType() == typeof(decimal))
                    {
                        this._value = Convert.ToDouble(value);
                    }
                    else
                    {
                        this._value = value;
                    }
                }
            }
        }

        [JsonIgnore]
        public object DisplayValue { get; set; }

        [JsonIgnore]
        public bool Bold
        {
            get
            {
                return this._bold;
            }

            set
            {
                if (value)
                {
                    this.TDClass += " bold";
                }

                this._bold = value;
            }
        }

        public int MergeRowNumber { get; set; }

        public int MergeColNumber { get; set; }

        [JsonProperty("tdClass")]
        public string TDClass { get; set; }

        public string CssClass { get; set; }

        [JsonProperty("customAttr")]
        public string CustomAttributes { get; set; }

        [JsonIgnore]
        public HAlignment CellAlignment { get; set; }

        public string CellAlign
        {
            get
            {
                switch (this.CellAlignment)
                {
                    case HAlignment.Left:
                        return "left";
                    case HAlignment.Right:
                        return "right";
                    case HAlignment.Center:
                        return "center";
                    default:
                        return string.Empty;
                }
            }
        }

        [JsonIgnore]
        public string RichTextString { get; set; }

        public List<int> StartIndex { get; set; }

        public List<int> EndIndex { get; set; }

        public List<ExcelFont> TextFont { get; set; }

        [JsonIgnore]
        public ExcelCellType CustomCellType { get; set; }

        [JsonIgnore]
        public int CustomExcelCellStyleIndex { get; set; }

        [JsonIgnore]
        public bool WrapText { get; set; }

        [JsonIgnore]
        public bool HiddenColumn { get; set; }

        [JsonIgnore]
        public short Indention { get; set; }

        public bool ShouldSerializeMergeRowNumber()
        {
            return MergeRowNumber > 1;
        }

        public bool ShouldSerializeMergeColNumber()
        {
            return MergeColNumber > 1;
        }

        public bool ShouldSerializeTDClass()
        {
            return !string.IsNullOrEmpty(TDClass);
        }

        /// <summary>
        /// Gets the reference header column.
        /// </summary>
        /// <param name="root">The parent object.</param>
        /// <returns>
        /// Header Columns object.
        /// </returns>
        public HeaderColumns ReferenceHeaderColumn(GridDataRow root = null)
        {
            if (root != null)
            {
                this.GridDataRow = root;
            }

            if (this.GridDataRow != null && this.GridDataRow.GridOptions != null)
            {
                int headerColumnsCount = this.GridDataRow.GridOptions.Columns.Count;
                for (int i = 0; i < headerColumnsCount; i++)
                {
                    if (this.Name == this.GridDataRow.GridOptions.Columns[i].Name)
                    {
                        return this.GridDataRow.GridOptions.Columns[i];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Columns object.</returns>
        public Columns Clone()
        {
            Columns clone = new Columns(this.ColumnIndex);
            clone.Name = this.Name;
            clone.Value = this.Value;
            clone.DisplayValue = this.DisplayValue;
            clone.Bold = this.Bold;
            clone.MergeRowNumber = this.MergeRowNumber;
            clone.MergeColNumber = this.MergeColNumber;
            clone.TDClass = this.TDClass;
            clone.CustomAttributes = this.CustomAttributes;
            clone.CellAlignment = this.CellAlignment;
            clone.RichTextString = this.RichTextString;
            clone.WrapText = this.WrapText;
            clone.HiddenColumn = this.HiddenColumn;
            clone.Indention = this.Indention;
            int startIndexCount = this.StartIndex.Count;
            int endIndexCount = this.EndIndex.Count;
            int textFontCount = this.TextFont.Count;
            int i = 0;
            for (; i < startIndexCount; i++)
            {
                clone.StartIndex.Add(this.StartIndex[i]);
            }

            for (i = 0; i < endIndexCount; i++)
            {
                clone.EndIndex.Add(this.EndIndex[i]);
            }

            for (i = 0; i < textFontCount; i++)
            {
                clone.TextFont.Add(this.TextFont[i]);
            }

            clone.CustomCellType = this.CustomCellType;
            return clone;
        }

        private bool CheckValidValueType(Type type)
        {
            return type.Equals(typeof(bool))
                || type.Equals(typeof(string))
                || type.Equals(typeof(int))
                || type.Equals(typeof(long))
                || type.Equals(typeof(double))
                || type.Equals(typeof(decimal))
                || type.Equals(typeof(DateTime))
                || type.Equals(typeof(IRichTextString));
        }
    }
}
