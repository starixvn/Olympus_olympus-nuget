// -----------------------------------------------------------------------
// <copyright file="OddStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.DoubleStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LowerStyle : FooterBaseCellStyle
    {
        public LowerStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.Double,
                new GrandTotalRowColor(),
                new XSSFColor(ExcelConstants.FooterBackgroundColor))
        {
        }
    }
}
