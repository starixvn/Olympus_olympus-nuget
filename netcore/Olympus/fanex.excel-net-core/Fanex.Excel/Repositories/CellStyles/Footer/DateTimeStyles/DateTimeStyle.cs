// -----------------------------------------------------------------------
// <copyright file="DateTimeStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.DateTimeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DateTimeStyle : BaseCellStyles
    {
        public DateTimeStyle(IWorkbook workbook, IFont font)
            : base(workbook)
        {
            this.Style = this.CreateCellStyle(
                ExcelConstants.DateTimemmddyyyy,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new GrandTotalRowColor() : (IColor)(new XSSFColor(ExcelConstants.FooterBackgroundColor)),
                font,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);

            this.UpperStyle = this.CreateCellStyle(
                ExcelConstants.DateTimemmddyyyy,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new GrandTotalRowColor() : (IColor)(new XSSFColor(ExcelConstants.FooterBackgroundColor)),
                font,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);

            this.UpperStyle.BorderTop = BorderStyle.Thick;
            this.UpperStyle.TopBorderColor = HSSFColor.Orange.Index;
        }

        public ICellStyle UpperStyle { get; set; }
    }
}
