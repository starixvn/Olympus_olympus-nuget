// -----------------------------------------------------------------------
// <copyright file="ParentLevelFiveStyle.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Body.DoubleRedParenthesisNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ParentLevelFiveStyle : BodyBaseCellStyle
    {
        public ParentLevelFiveStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.DoubleRedParenthesisNegative,
                new ParentLevelFiveColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelFiveColor))
        {
        }
    }
}