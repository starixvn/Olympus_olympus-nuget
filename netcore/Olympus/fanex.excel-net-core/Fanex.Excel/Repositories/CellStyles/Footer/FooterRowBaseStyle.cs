
namespace Fanex.Excel.Repositories.CellStyles.Footer
{
    using Fanex.Excel.Enums;
    using NPOI.SS.UserModel;

    public class FooterRowBaseStyle : BaseCellStyles
    {
        public FooterRowBaseStyle(IWorkbook workbook, IFont font)
            : base(workbook)
        {
        }

        public BaseCellStyles Upper { get; set; }

        public BaseCellStyles Lower { get; set; }

        public ICellStyle GetCellStyle(FooterRowType rowType, HAlignment align = HAlignment.Null)
        {
            switch (rowType)
            {
                case FooterRowType.Upper:
                    return this.Upper.GetCellStyleByHAlign(align);
                default:
                    return this.Lower.GetCellStyleByHAlign(align);
            }
        }
    }
}
