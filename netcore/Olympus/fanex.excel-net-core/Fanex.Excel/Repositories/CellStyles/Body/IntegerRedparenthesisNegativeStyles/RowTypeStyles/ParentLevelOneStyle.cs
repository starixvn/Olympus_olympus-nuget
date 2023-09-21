
namespace Fanex.Excel.Repositories.CellStyles.Body.IntegerRedparenthesisNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class ParentLevelOneStyle : BodyBaseCellStyle
    {
        public ParentLevelOneStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.IntegerRedparenthesisNegative,
                new ParentLevelOneColor(),
                new XSSFColor(ExcelConstants.ParentRowLevelOneColor))
        {
        }
    }
}
