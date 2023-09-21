
namespace Fanex.Excel.Repositories.CellStyles.Body.PercentageStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class ParentLevelTwoStyle : BodyBaseCellStyle
    {
        public ParentLevelTwoStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.Percentage,
                new ParentLevelTwoColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelTwoColor))
        {
        }
    }
}
