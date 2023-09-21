
namespace Fanex.Excel.Repositories.CellStyles
{
    using Fanex.Excel.Enums;
    using NPOI.SS.UserModel;

    public interface IBodyCellStyles
    {
        ICellStyle GetCellStyle(Enums.ExcelCellType cellType, Enums.HAlignment horizonAlign, RowType rowType, bool isBold = false);

        ICellStyle GetGeneralCellStyle(Enums.HAlignment horizonAlign, RowType rowType, bool isBold = false);
    }
}
