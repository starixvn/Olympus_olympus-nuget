namespace Fanex.Excel.Repositories.CellStyles.Body
{
    
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// Body Base Cell Style.
    /// </summary>
    public class BodyBaseCellStyle : BaseCellStyles
    {
        public BodyBaseCellStyle(IWorkbook workbook, IFont font, string dataFormat, HSSFColor hssfColor, XSSFColor xssfColor)
            : base(workbook)
        {
            this.Style = this.CreateCellStyle(
                dataFormat,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)hssfColor : (IColor)xssfColor,
                font,
                HorizontalAlignment.General,
                VerticalAlignment.Top);

            this.LeftStyle = this.CreateCellStyle(
                dataFormat,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)hssfColor : (IColor)xssfColor,
                font,
                HorizontalAlignment.Left,
                VerticalAlignment.Top);

            this.RightStyle = this.CreateCellStyle(
                dataFormat,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)hssfColor : (IColor)xssfColor,
                font,
                HorizontalAlignment.Right,
                VerticalAlignment.Top);

            this.CenterStyle = this.CreateCellStyle(
                dataFormat,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)hssfColor : (IColor)xssfColor,
                font,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);
        }
    }
}
