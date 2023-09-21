
namespace Fanex.Excel.Repositories.CellStyles.Body.DoubleRedNegativeStyles.RowTypeStyles
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
                ExcelConstants.DoubleRedNegative,
                new EvenRowColor(),
                new XSSFColor(ExcelConstants.EvenRowColor))
        {
        }
    }
}
