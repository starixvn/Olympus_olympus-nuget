
namespace Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedNegativeStyles
{
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class DoubleRedNegativeStyle : FooterRowBaseStyle
    {
        public DoubleRedNegativeStyle(IWorkbook workbook, IFont font)
             : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
