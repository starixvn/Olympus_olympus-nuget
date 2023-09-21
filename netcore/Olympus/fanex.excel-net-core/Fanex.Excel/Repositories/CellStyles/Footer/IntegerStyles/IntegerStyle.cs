
namespace Fanex.Excel.Repositories.CellStyles.Footer.IntegerStyles
{
    using Body;
    using Body.IntegerStyles.RowTypeStyles;
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class IntegerStyle : FooterRowBaseStyle
    {
        public IntegerStyle(IWorkbook workbook, IFont font)
             : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
