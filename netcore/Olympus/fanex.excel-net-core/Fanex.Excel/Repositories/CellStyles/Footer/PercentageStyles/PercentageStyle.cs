
namespace Fanex.Excel.Repositories.CellStyles.Footer.PercentageStyles
{
    using Body.PercentageStyles.RowTypeStyles;
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class PercentageStyle : FooterRowBaseStyle
    {
        public PercentageStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
