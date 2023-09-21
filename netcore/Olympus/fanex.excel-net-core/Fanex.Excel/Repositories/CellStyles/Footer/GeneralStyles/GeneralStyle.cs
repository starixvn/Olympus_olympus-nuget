
namespace Fanex.Excel.Repositories.CellStyles.Footer.GeneralStyles
{
    using Fanex.Excel.Repositories.CellStyles.Footer.GeneralStyles.InfomationStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.GeneralStyles.LeftStyles;
    using NPOI.SS.UserModel;

    public class GeneralStyle : BaseCellStyles
    {
        public GeneralStyle(IWorkbook workbook, IFont font)
            : base(workbook)
        {
            this.Left = new LeftStyle(workbook, font);
            this.Right = new RightStyle(workbook, font);
            this.Center = new CenterStyle(workbook, font);
            this.Info = new InfomationStyle(workbook, font);
        }

        public LeftStyle Left { get; set; }

        public RightStyle Right { get; set; }

        public CenterStyle Center { get; set; }

        public InfomationStyle Info { get; set; }
    }
}
