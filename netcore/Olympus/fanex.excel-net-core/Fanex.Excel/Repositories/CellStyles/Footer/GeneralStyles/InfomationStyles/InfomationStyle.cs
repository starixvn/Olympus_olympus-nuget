
namespace Fanex.Excel.Repositories.CellStyles.Footer.GeneralStyles.InfomationStyles
{
    
    using Fanex.Excel.Constants;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using SixLabors.ImageSharp;

    public class InfomationStyle : BaseCellStyles
    {
        public InfomationStyle(IWorkbook workbook, IFont font)
            : base(workbook)
        {
            this.Style = this.CreateCellStyle(
                ExcelConstants.General,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new HSSFColor.White() : (IColor)(new XSSFColor(Color.Transparent)),
                font,
                HorizontalAlignment.Left,
                VerticalAlignment.Top);
        }
    }
}
