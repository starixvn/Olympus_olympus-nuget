// -----------------------------------------------------------------------
// <copyright file="ParentLevelTwoStyle.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Body.DoubleRedNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ParentLevelTwoStyle : BodyBaseCellStyle
    {
        public ParentLevelTwoStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.DoubleRedNegative,
                new ParentLevelTwoColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelTwoColor))
        {
        }
    }
}