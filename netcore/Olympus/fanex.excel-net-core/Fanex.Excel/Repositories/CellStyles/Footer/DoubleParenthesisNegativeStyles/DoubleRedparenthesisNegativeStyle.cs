namespace Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedparenthesisNegativeStyles
{
    using DoubleParenthesisNegativeStyles.RowTypeStyles;
    using NPOI.SS.UserModel;

    public class DoubleParenthesisNegativeStyle : FooterRowBaseStyle
    {
        public DoubleParenthesisNegativeStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}