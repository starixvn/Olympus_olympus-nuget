// -----------------------------------------------------------------------
// <copyright file="ParentLevelFourStyle.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
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
    public class ParentLevelFourStyle : BodyBaseCellStyle
    {
        public ParentLevelFourStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.IntegerRedPositive,
                new ParentLevelFourColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelFourColor))
        {
        }
    }
}