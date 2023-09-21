
namespace Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedNegativeStyles
{
    using Body;
    using Body.PercentageRedNegativeStyles.RowTypeStyles;
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class PercentageRedNegativeStyle : FooterRowBaseStyle
    {
        public PercentageRedNegativeStyle(IWorkbook workbook, IFont font)
           : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
