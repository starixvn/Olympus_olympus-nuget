
namespace Fanex.Excel.Repositories.CellStyles.Body.DoubleRedNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class ParentLevelFiveStyle : BodyBaseCellStyle
    {
        public ParentLevelFiveStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.DoubleRedNegative,
                new ParentLevelFiveColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelFiveColor))
        {
        }
    }
}
