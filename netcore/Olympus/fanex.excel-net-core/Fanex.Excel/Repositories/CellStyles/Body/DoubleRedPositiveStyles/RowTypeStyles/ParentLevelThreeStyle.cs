
namespace Fanex.Excel.Repositories.CellStyles.Body.DoubleRedPositiveStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class ParentLevelThreeStyle : BodyBaseCellStyle
    {
        public ParentLevelThreeStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.DoubleRedPositive,
                new ParentLevelThreeColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelThreeColor))
        {
        }
    }
}
