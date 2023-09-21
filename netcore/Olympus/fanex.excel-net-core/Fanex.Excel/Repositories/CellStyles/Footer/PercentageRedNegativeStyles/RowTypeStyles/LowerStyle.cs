
namespace Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class LowerStyle : FooterBaseCellStyle
    {
        public LowerStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.PercentageRedNegative,
                new GrandTotalRowColor(),
                new XSSFColor(ExcelConstants.FooterBackgroundColor))
        {
        }
    }
}
