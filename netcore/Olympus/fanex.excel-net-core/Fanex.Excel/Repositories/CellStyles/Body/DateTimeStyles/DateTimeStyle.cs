
namespace Fanex.Excel.Repositories.CellStyles.Body.DateTimeStyles
{
    using Fanex.Excel.Repositories.CellStyles.Body.DateTimeStyles.RowTypeStyles;
    using NPOI.SS.UserModel;

    public class DateTimeStyle : BodyRowBaseStyle
    {
        public DateTimeStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Odd = new OddStyle(workbook, font);
            this.Even = new EvenStyle(workbook, font);
            this.SubTotal = new SubTotalStyle(workbook, font);
            this.ParentLevelOne = new ParentLevelOneStyle(workbook, font);
            this.ParentLevelTwo = new ParentLevelTwoStyle(workbook, font);
            this.ParentLevelThree = new ParentLevelThreeStyle(workbook, font);
            this.ParentLevelFour = new ParentLevelFourStyle(workbook, font);
            this.ParentLevelFive = new ParentLevelFiveStyle(workbook, font);
        }
    }
}
