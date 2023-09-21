
namespace Fanex.Excel.Repositories.CellStyles.Body
{
    using Fanex.Excel.Enums;
    using NPOI.SS.UserModel;

    public class BodyRowBaseStyle : BaseCellStyles
    {
        public BodyRowBaseStyle(IWorkbook workbook, IFont font)
            : base(workbook)
        {
        }

        public BaseCellStyles Odd { get; set; }

        public BaseCellStyles Even { get; set; }

        public BaseCellStyles SubTotal { get; set; }

        public BaseCellStyles ParentLevelOne { get; set; }

        public BaseCellStyles ParentLevelTwo { get; set; }

        public BaseCellStyles ParentLevelThree { get; set; }

        public BaseCellStyles ParentLevelFour { get; set; }

        public BaseCellStyles ParentLevelFive { get; set; }

        public ICellStyle GetCellStyle(RowType rowType, HAlignment align = HAlignment.Null)
        {
            switch (rowType)
            {
                case RowType.Even:
                    return this.Even.GetCellStyleByHAlign(align);
                case RowType.SubTotal:
                    return this.SubTotal.GetCellStyleByHAlign(align);
                case RowType.ParentLevelOne:
                    return this.ParentLevelOne.GetCellStyleByHAlign(align);
                case RowType.ParentLevelTwo:
                    return this.ParentLevelTwo.GetCellStyleByHAlign(align);
                case RowType.ParentLevelThree:
                    return this.ParentLevelThree.GetCellStyleByHAlign(align);
                case RowType.ParentLevelFour:
                    return this.ParentLevelFour.GetCellStyleByHAlign(align);
                case RowType.ParentLevelFive:
                    return this.ParentLevelFive.GetCellStyleByHAlign(align);
                default:
                    return this.Odd.GetCellStyleByHAlign(align);
            }
        }
    }
}
