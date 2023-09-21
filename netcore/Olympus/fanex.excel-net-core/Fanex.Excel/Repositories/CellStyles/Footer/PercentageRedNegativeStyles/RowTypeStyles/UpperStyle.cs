// -----------------------------------------------------------------------
// <copyright file="EvenStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UpperStyle : FooterBaseCellStyle
    {
        public UpperStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.PercentageRedNegative,
                new GrandTotalRowColor(),
                new XSSFColor(ExcelConstants.FooterBackgroundColor))
        {
            this.Style.BorderTop = this.LeftStyle.BorderTop = this.RightStyle.BorderTop = this.CenterStyle.BorderTop = BorderStyle.Thick;
            this.Style.TopBorderColor = this.LeftStyle.TopBorderColor = this.RightStyle.TopBorderColor = this.CenterStyle.TopBorderColor = HSSFColor.Orange.Index;
        }
    }
}
