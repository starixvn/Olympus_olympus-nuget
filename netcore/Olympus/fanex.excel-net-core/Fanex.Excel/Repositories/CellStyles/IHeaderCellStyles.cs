
namespace Fanex.Excel.Repositories.CellStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    public interface IHeaderCellStyles
    {
        ICellStyle GetHeaderStyle(string alignment);
    }
}
