
namespace Fanex.Excel.Repositories.CellStyles.Body.DoubleStyles.RowTypeStyles
{
    
    using Fanex.Excel.Constants;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using SixLabors.ImageSharp;

    public class OddStyle : BodyBaseCellStyle
    {
        public OddStyle(IWorkbook workbook, IFont font)
            : base(
                workbook,
                font,
                ExcelConstants.Double,
                new HSSFColor.White(),
                new XSSFColor(Color.Transparent))
        {
        }
    }
}
