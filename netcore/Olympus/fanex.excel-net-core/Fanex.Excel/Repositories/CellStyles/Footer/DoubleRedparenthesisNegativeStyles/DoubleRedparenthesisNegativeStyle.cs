
namespace Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedparenthesisNegativeStyles
{
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class DoubleRedparenthesisNegativeStyle : FooterRowBaseStyle
    {
        public DoubleRedparenthesisNegativeStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
