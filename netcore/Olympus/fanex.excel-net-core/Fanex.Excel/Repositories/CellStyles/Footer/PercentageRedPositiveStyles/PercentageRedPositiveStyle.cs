
namespace Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedPositiveStyles
{
    using Body.PercentageRedPositiveStyles.RowTypeStyles;
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class PercentageRedPositiveStyle : FooterRowBaseStyle
    {
        public PercentageRedPositiveStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
