// -----------------------------------------------------------------------
// <copyright file="ParentLevelOneStyle.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Body.PercentageStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ParentLevelOneStyle : BodyBaseCellStyle
    {
        public ParentLevelOneStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.Percentage,
                new ParentLevelOneColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelOneColor))
        {
        }
    }
}