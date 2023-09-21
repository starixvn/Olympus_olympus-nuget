
namespace Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedPositiveStyles
{
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class DoubleRedPositiveStyle : FooterRowBaseStyle
    {
        public DoubleRedPositiveStyle(IWorkbook workbook, IFont font)
             : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
