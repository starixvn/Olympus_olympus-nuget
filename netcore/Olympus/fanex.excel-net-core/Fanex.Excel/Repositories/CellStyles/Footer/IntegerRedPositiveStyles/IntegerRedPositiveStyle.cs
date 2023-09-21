
namespace Fanex.Excel.Repositories.CellStyles.Footer.IntegerRedPositiveStyles
{
    using IntegerRedPositiveStyles.RowTypeStyles;
    using NPOI.SS.UserModel;

    public class IntegerRedPositiveStyle : FooterRowBaseStyle
    {
        public IntegerRedPositiveStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
