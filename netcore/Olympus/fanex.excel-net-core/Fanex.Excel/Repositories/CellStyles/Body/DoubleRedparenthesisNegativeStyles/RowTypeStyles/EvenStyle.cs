
namespace Fanex.Excel.Repositories.CellStyles.Body.DoubleRedParenthesisNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class EvenStyle : BodyBaseCellStyle
    {
        public EvenStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.DoubleRedParenthesisNegative,
                new EvenRowColor(),
                new XSSFColor(ExcelConstants.EvenRowColor))
        {
        }
    }
}
