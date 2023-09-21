
namespace Fanex.Excel.Repositories.CellStyles.Body.PercentageRedPositiveStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class SubTotalStyle : BodyBaseCellStyle
    {
        public SubTotalStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.PercentageRedPositive,
                new SubTotalRowColor(),
                new XSSFColor(ExcelConstants.SubTotalRowColor))
        {
        }
    }
}
