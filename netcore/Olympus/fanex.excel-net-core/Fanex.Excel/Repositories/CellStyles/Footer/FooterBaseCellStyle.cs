namespace Fanex.Excel.Repositories.CellStyles.Footer
{
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using Fanex.Excel.Repositories.CellStyles.Body;

    /// <summary>
    /// Body Base Cell Style.
    /// </summary>
    public class FooterBaseCellStyle : BodyBaseCellStyle
    {
        public FooterBaseCellStyle(IWorkbook workbook, IFont font, string dataFormat, HSSFColor hssfColor, XSSFColor xssfColor)
            : base(workbook, font, dataFormat, hssfColor, xssfColor)
        {
        }
    }
}
