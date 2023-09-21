
namespace Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedparenthesisNegativeStyles
{
    using Body.PercentageRedparenthesisNegativeStyles.RowTypeStyles;
    using NPOI.SS.UserModel;
    using RowTypeStyles;
    public class PercentageRedparenthesisNegativeStyle : FooterRowBaseStyle
    {
        public PercentageRedparenthesisNegativeStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
