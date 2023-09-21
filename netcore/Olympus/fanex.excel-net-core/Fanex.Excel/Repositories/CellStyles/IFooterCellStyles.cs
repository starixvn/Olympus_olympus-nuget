
namespace Fanex.Excel.Repositories.CellStyles
{
    using NPOI.SS.UserModel;

    public interface IFooterCellStyles
    {
        ICellStyle GetCellStyle(Enums.ExcelCellType cellType, Enums.HAlignment horizonAlign, bool isFirstFooterRow);

        ICellStyle GetGeneralCellStyle(Enums.HAlignment horizonAlign, bool isFirstFooterRow);
    }
}
