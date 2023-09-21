namespace Fanex.Excel.Models
{
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;

    public class CustomExcelCellStyle
    {
        public CustomExcelFont Font { get; set; }

        public HorizontalAlignment HAlign { get; set; }

        public VerticalAlignment VAlign { get; set; }

        public short BackgroundColor { get; set; }

        public string DataFormat { get; set; }

        public bool WrapText { get; set; }

        public CustomExcelCellStyle()
        {
            this.HAlign = HorizontalAlignment.Left;
            this.VAlign = VerticalAlignment.Center;
            this.WrapText = false;
        }

        public ICellStyle ToNPOICellStyle(IWorkbook workbook)
        {
            var customCellStyle = workbook.CreateCellStyle();
            customCellStyle.SetFont(Font.ToNPOIFont(workbook));
            customCellStyle.VerticalAlignment = VAlign;
            customCellStyle.Alignment = HAlign;
            if(DataFormat != null)
            {
                customCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat(DataFormat);
            }
            
            customCellStyle.FillPattern = FillPattern.SolidForeground;
            customCellStyle.FillForegroundColor = this.BackgroundColor;
            customCellStyle.WrapText = this.WrapText;

            customCellStyle.BorderTop = BorderStyle.Thin;
            customCellStyle.TopBorderColor = HSSFColor.Black.Index;
            customCellStyle.BorderLeft = BorderStyle.Thin;
            customCellStyle.LeftBorderColor = HSSFColor.Black.Index;
            customCellStyle.BorderBottom = BorderStyle.Thin;
            customCellStyle.BottomBorderColor = HSSFColor.Black.Index;
            customCellStyle.BorderRight = BorderStyle.Thin;
            customCellStyle.RightBorderColor = HSSFColor.Black.Index;
            return customCellStyle;
        }
    }

    public class CustomExcelFont
    {
        public string FontName { get; set; }

        public short FontHeightInPoints { get; set; }

        public bool IsBold { get; set; }

        public bool IsItalic { get; set; }

        public short ForegroundColor { get; set; }

        public CustomExcelFont()
        {
            this.FontName = "Tahoma";
            this.FontHeightInPoints = 8;
            this.IsBold = false;
            this.IsItalic = false;
            this.ForegroundColor = 0;
        }

        public IFont ToNPOIFont(IWorkbook workbook)
        {
            var font = workbook.CreateFont();
            font.FontName = this.FontName;
            font.FontHeightInPoints = this.FontHeightInPoints;
            font.IsBold = this.IsBold;
            font.IsItalic = this.IsItalic;
            font.Color = this.ForegroundColor;
            return font;
        }
    }
}
