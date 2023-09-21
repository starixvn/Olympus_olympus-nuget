// -----------------------------------------------------------------------
// <copyright file="BaseCellStyles.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles
{
    using Fanex.Excel.Enums;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseCellStyles
    {
        public BaseCellStyles(IWorkbook workbook)
        {
            this.Workbook = workbook;
            this.DataFormat = this.Workbook.CreateDataFormat();
        }

        public BaseCellStyles(IWorkbook workbook, IFont font)
            : this(workbook)
        {
            this.Font = font;
        }

        public ICellStyle Style { get; set; }

        public ICellStyle LeftStyle { get; set; }

        public ICellStyle RightStyle { get; set; }

        public ICellStyle CenterStyle { get; set; }

        protected IWorkbook Workbook { get; set; }

        protected IFont Font { get; set; }

        protected IDataFormat DataFormat { get; set; }

        /// <summary>
        /// Gets the horizontal alignment.
        /// </summary>
        /// <param name="horizonAlign">The horizon align.</param>
        /// <returns>The value of HorizontalAlignment.</returns>
        public ICellStyle GetCellStyleByHAlign(HAlignment horizonAlign)
        {
            switch (horizonAlign)
            {
                case HAlignment.Left:
                    return this.LeftStyle;
                case HAlignment.Right:
                    return this.RightStyle;
                case HAlignment.Center:
                    return this.CenterStyle;
                default:
                    return this.Style;
            }
        }

        /// <summary>
        /// Creates the cell style.
        /// </summary>
        /// <param name="dataFormat">The data format.</param>
        /// <param name="colorBG">The color background color.</param>
        /// <param name="font">The current font.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="valignment">The vertical alignment.</param>
        /// <returns>Cell style.</returns>
        protected ICellStyle CreateCellStyle(string dataFormat, IColor colorBG, IFont font, HorizontalAlignment alignment, VerticalAlignment valignment)
        {
            if (dataFormat == null || string.IsNullOrEmpty(dataFormat.Trim()))
            {
                dataFormat = "General";
            }

            if (Workbook.GetType().Equals(typeof(XSSFWorkbook)))
            {
                return this.CreateCellStyleXSSF(dataFormat, (XSSFColor)colorBG, font, alignment, valignment);
            }

            return this.CreateCellStyleHSSF(dataFormat, (HSSFColor)colorBG, font, alignment, valignment);
        }

        /// <summary>
        /// Creates the cell style XSSF.
        /// </summary>
        /// <param name="dataFormat">The data format.</param>
        /// <param name="colorBG">The color background color.</param>
        /// <param name="font">The current font.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="valignment">The vertical alignment.</param>
        /// <returns>Cell Style.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "This suppress is OK.")]
        protected ICellStyle CreateCellStyleXSSF(string dataFormat, XSSFColor colorBG, IFont font, HorizontalAlignment alignment, VerticalAlignment valignment)
        {
            var cellStyle = (XSSFCellStyle)this.Workbook.CreateCellStyle();
            cellStyle.DataFormat = this.DataFormat.GetFormat(dataFormat);
            cellStyle.FillForegroundXSSFColor = colorBG;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.SetFont(font);
            cellStyle.Alignment = alignment;
            cellStyle.VerticalAlignment = valignment;
            this.SetCellBorder(cellStyle);
            return cellStyle;
        }

        /// <summary>
        /// Creates the cell style HSSF.
        /// </summary>
        /// <param name="dataFormat">The data format.</param>
        /// <param name="colorBG">The color background color.</param>
        /// <param name="font">The current font.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="valignment">The vertical alignment.</param>
        /// <returns>Cell Style.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "This Suppress is OK.")]
        protected ICellStyle CreateCellStyleHSSF(string dataFormat, HSSFColor colorBG, IFont font, HorizontalAlignment alignment, VerticalAlignment valignment)
        {
            var cellStyle = this.Workbook.CreateCellStyle();
            cellStyle.DataFormat = this.DataFormat.GetFormat(dataFormat);
            cellStyle.FillForegroundColor = colorBG.Indexed;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.SetFont(font);
            cellStyle.Alignment = alignment;
            cellStyle.VerticalAlignment = valignment;
            this.SetCellBorder(cellStyle);
            return cellStyle;
        }

        private void SetCellBorder(ICellStyle cellStyle)
        {
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.TopBorderColor = HSSFColor.Black.Index;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.LeftBorderColor = HSSFColor.Black.Index;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BottomBorderColor = HSSFColor.Black.Index;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.RightBorderColor = HSSFColor.Black.Index;
        }
    }
}
