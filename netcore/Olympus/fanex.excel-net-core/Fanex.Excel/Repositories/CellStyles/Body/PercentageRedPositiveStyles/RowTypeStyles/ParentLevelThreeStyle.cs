// -----------------------------------------------------------------------
// <copyright file="ParentLevelThreeStyle.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Body.PercentageRedPositiveStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ParentLevelThreeStyle : BodyBaseCellStyle
    {
        public ParentLevelThreeStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.PercentageRedPositive,
                new ParentLevelThreeColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelThreeColor))
        {
        }
    }
}