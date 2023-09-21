
namespace Fanex.Excel.Repositories.CellStyles.Body.IntegerRedNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class ParentLevelFourStyle : BodyBaseCellStyle
    {
        public ParentLevelFourStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.IntegerRedNegative,
                new ParentLevelFourColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelFourColor))
        {
        }
    }
}
