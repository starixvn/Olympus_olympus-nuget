// -----------------------------------------------------------------------
// <copyright file="EvenStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Body.IntegerRedPositiveStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EvenStyle : BodyBaseCellStyle
    {
        public EvenStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.IntegerRedPositive,
                new EvenRowColor(),
                new XSSFColor(ExcelConstants.EvenRowColor))
        {
        }
    }
}
