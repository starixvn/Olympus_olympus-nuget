
namespace Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedparenthesisNegativeStyles.RowTypeStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public class UpperStyle : FooterBaseCellStyle
    {
        public UpperStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.DoubleRedParenthesisNegative,
                new GrandTotalRowColor(),
                new XSSFColor(ExcelConstants.FooterBackgroundColor))
        {
            this.Style.BorderTop = this.LeftStyle.BorderTop = this.RightStyle.BorderTop = this.CenterStyle.BorderTop = BorderStyle.Thick;
            this.Style.TopBorderColor = this.LeftStyle.TopBorderColor = this.RightStyle.TopBorderColor = this.CenterStyle.TopBorderColor = HSSFColor.Orange.Index;
        }
    }
}
