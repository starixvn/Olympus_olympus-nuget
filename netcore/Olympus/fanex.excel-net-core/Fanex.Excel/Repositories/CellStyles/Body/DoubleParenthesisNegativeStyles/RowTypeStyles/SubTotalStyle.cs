// -----------------------------------------------------------------------
// <copyright file="SubTotalStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Body.DoubleParenthesisNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SubTotalStyle : BodyBaseCellStyle
    {
        public SubTotalStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.DoubleParenthesisNegative,
                new SubTotalRowColor(),
                new XSSFColor(ExcelConstants.SubTotalRowColor))
        {
        }
    }
}
