
namespace Fanex.Excel.Repositories.CellStyles
{
    using Fanex.Excel.Enums;
    using Fanex.Excel.Repositories.CellStyles.Body.DateTimeStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.DoubleParenthesisNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.DoubleRedNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.DoubleRedParenthesisNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.DoubleRedPositiveStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.DoubleStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.GeneralStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.IntegerRedNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.IntegerRedparenthesisNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.IntegerRedPositiveStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.IntegerStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.PercentageRedNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.PercentageRedparenthesisNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.PercentageRedPositiveStyles;
    using Fanex.Excel.Repositories.CellStyles.Body.PercentageStyles;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;

    public class BodyCellStyles : BaseCellStyles, IBodyCellStyles
    {
        private IFont _bodyFont;

        private IFont _bodyBoldFont;

        public BodyCellStyles(IWorkbook workbook)
            : base(workbook)
        {
            this.Workbook = workbook;
            this.CreateFont();
            this.CreateBoldFont();
            this.General = new GeneralStyle(workbook, this._bodyFont);
            this.Integer = new IntegerStyle(workbook, this._bodyFont);
            this.IntegerRedPositive = new IntegerRedPositiveStyle(workbook, this._bodyFont);
            this.IntegerRedNegative = new IntegerRedNegativeStyle(workbook, this._bodyFont);
            this.IntegerRedparenthesisNegative = new IntegerRedparenthesisNegativeStyle(workbook, this._bodyFont);
            this.Double = new DoubleStyle(workbook, this._bodyFont);
            this.DoubleRedPositive = new DoubleRedPositiveStyle(workbook, this._bodyFont);
            this.DoubleRedNegative = new DoubleRedNegativeStyle(workbook, this._bodyFont);
            this.DoubleRedparenthesisNegative = new DoubleRedParenthesisNegativeStyle(workbook, this._bodyFont);
            this.Percentage = new PercentageStyle(workbook, this._bodyFont);
            this.PercentageRedPositive = new PercentageRedPositiveStyle(workbook, this._bodyFont);
            this.PercentageRedNegative = new PercentageRedNegativeStyle(workbook, this._bodyFont);
            this.PercentageRedparenthesisNegative = new PercentageRedparenthesisNegativeStyle(workbook, this._bodyFont);
            this.GeneralBold = new GeneralStyle(workbook, this._bodyBoldFont);
            this.IntegerBold = new IntegerStyle(workbook, this._bodyBoldFont);
            this.IntegerRedPositiveBold = new IntegerRedPositiveStyle(workbook, this._bodyBoldFont);
            this.IntegerRedNegativeBold = new IntegerRedNegativeStyle(workbook, this._bodyBoldFont);
            this.IntegerRedparenthesisNegativeBold = new IntegerRedparenthesisNegativeStyle(workbook, this._bodyBoldFont);
            this.DoubleBold = new DoubleStyle(workbook, this._bodyBoldFont);
            this.DoubleRedPositiveBold = new DoubleRedPositiveStyle(workbook, this._bodyBoldFont);
            this.DoubleRedNegativeBold = new DoubleRedNegativeStyle(workbook, this._bodyBoldFont);
            this.DoubleRedparenthesisNegativeBold = new DoubleRedParenthesisNegativeStyle(workbook, this._bodyBoldFont);
            this.PercentageBold = new PercentageStyle(workbook, this._bodyBoldFont);
            this.PercentageRedPositiveBold = new PercentageRedPositiveStyle(workbook, this._bodyBoldFont);
            this.PercentageRedNegativeBold = new PercentageRedNegativeStyle(workbook, this._bodyBoldFont);
            this.PercentageRedparenthesisNegativeBold = new PercentageRedparenthesisNegativeStyle(workbook, this._bodyBoldFont);
            this.DateTimeStyle = new DateTimeStyle(workbook, this._bodyFont);
            this.DateTimeStyleBold = new DateTimeStyle(workbook, this._bodyBoldFont);
            this.DoubleParenthesisNegative = new DoubleParenthesisNegativeStyle(workbook, this._bodyFont);
            this.DoubleParenthesisNegativeBold = new DoubleParenthesisNegativeStyle(workbook, this._bodyBoldFont);
        }

        public GeneralStyle General { get; set; }

        public GeneralStyle GeneralBold { get; set; }

        public IntegerStyle Integer { get; set; }

        public IntegerStyle IntegerBold { get; set; }

        public IntegerRedPositiveStyle IntegerRedPositive { get; set; }

        public IntegerRedPositiveStyle IntegerRedPositiveBold { get; set; }

        public IntegerRedNegativeStyle IntegerRedNegative { get; set; }

        public IntegerRedNegativeStyle IntegerRedNegativeBold { get; set; }

        public IntegerRedparenthesisNegativeStyle IntegerRedparenthesisNegative { get; set; }

        public IntegerRedparenthesisNegativeStyle IntegerRedparenthesisNegativeBold { get; set; }

        public DoubleStyle Double { get; set; }

        public DoubleStyle DoubleBold { get; set; }

        public DoubleRedPositiveStyle DoubleRedPositive { get; set; }

        public DoubleRedPositiveStyle DoubleRedPositiveBold { get; set; }

        public DoubleRedNegativeStyle DoubleRedNegative { get; set; }

        public DoubleRedNegativeStyle DoubleRedNegativeBold { get; set; }

        public DoubleRedParenthesisNegativeStyle DoubleRedparenthesisNegative { get; set; }

        public DoubleRedParenthesisNegativeStyle DoubleRedparenthesisNegativeBold { get; set; }

        public PercentageStyle Percentage { get; set; }

        public PercentageStyle PercentageBold { get; set; }

        public PercentageRedPositiveStyle PercentageRedPositive { get; set; }

        public PercentageRedPositiveStyle PercentageRedPositiveBold { get; set; }

        public PercentageRedNegativeStyle PercentageRedNegative { get; set; }

        public PercentageRedNegativeStyle PercentageRedNegativeBold { get; set; }

        public PercentageRedparenthesisNegativeStyle PercentageRedparenthesisNegative { get; set; }

        public PercentageRedparenthesisNegativeStyle PercentageRedparenthesisNegativeBold { get; set; }

        public DateTimeStyle DateTimeStyle { get; set; }

        public DateTimeStyle DateTimeStyleBold { get; set; }

        public DoubleParenthesisNegativeStyle DoubleParenthesisNegative { get; set; }

        public DoubleParenthesisNegativeStyle DoubleParenthesisNegativeBold { get; set; }

        public ICellStyle GetCellStyle(ExcelCellType cellType, HAlignment horizonAlign, RowType rowType, bool isBold = false)
        {
            switch (cellType)
            {
                case ExcelCellType.Integer:
                case ExcelCellType.IntegerRedPositive:
                case ExcelCellType.IntegerRedNegative:
                case ExcelCellType.IntegerRedparenthesisNegative:
                    return this.GetIntegerCellStyle(horizonAlign, cellType, rowType, isBold);
                case ExcelCellType.Double:
                case ExcelCellType.DoubleRedPositive:
                case ExcelCellType.DoubleRedNegative:
                case ExcelCellType.DoubleRedParenthesisNegative:
                case ExcelCellType.DoubleParenthesisNegative:
                    return this.GetDoubleCellStyle(horizonAlign, cellType, rowType, isBold);
                case ExcelCellType.Percentage:
                case ExcelCellType.PercentageRedPositive:
                case ExcelCellType.PercentageRedNegative:
                case ExcelCellType.PercentageRedparenthesisNegative:
                    return this.GetPercentageCellStyle(horizonAlign, cellType, rowType, isBold);
                case ExcelCellType.DateTimeMMDDYYYY:
                    return this.GetDateTimeCellStyle(horizonAlign, rowType, isBold);
                default:
                    return this.GetGeneralCellStyle(horizonAlign, rowType, isBold);
            }
        }

        public ICellStyle GetGeneralCellStyle(HAlignment horizonAlign, RowType rowType, bool isBold = false)
        {
            var generalStyle = isBold || rowType == RowType.SubTotal ? this.GeneralBold : this.General;
            return generalStyle.GetCellStyle(rowType, horizonAlign);
        }

        private ICellStyle GetIntegerCellStyle(HAlignment horizonAlign, ExcelCellType cellType, RowType rowType, bool isBold)
        {
            switch (cellType)
            {
                case Enums.ExcelCellType.Integer:
                    var integerStyle = isBold || rowType == RowType.SubTotal ? this.IntegerBold : this.Integer;
                    return integerStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.IntegerRedPositive:
                    var integerRedPositiveStyle = isBold || rowType == RowType.SubTotal ? this.IntegerRedPositiveBold : this.IntegerRedPositive;
                    return integerRedPositiveStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.IntegerRedNegative:
                    var integerRedNegativeStyle = isBold || rowType == RowType.SubTotal ? this.IntegerRedNegativeBold : this.IntegerRedNegative;
                    return integerRedNegativeStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.IntegerRedparenthesisNegative:
                    var integerRedparenthesisNegativeStyle = isBold || rowType == RowType.SubTotal ? this.IntegerRedparenthesisNegativeBold : this.IntegerRedparenthesisNegative;
                    return integerRedparenthesisNegativeStyle.GetCellStyle(rowType, horizonAlign);
                default:
                    return null;
            }
        }

        private ICellStyle GetDoubleCellStyle(HAlignment horizonAlign, Enums.ExcelCellType cellType, RowType rowType, bool isBold)
        {
            switch (cellType)
            {
                case Enums.ExcelCellType.Double:
                    var doubleStyle = isBold || rowType == RowType.SubTotal ? this.DoubleBold : this.Double;
                    return doubleStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.DoubleRedPositive:
                    var doubleRedPositiveStyle = isBold || rowType == RowType.SubTotal ? this.DoubleRedPositiveBold : this.DoubleRedPositive;
                    return doubleRedPositiveStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.DoubleRedNegative:
                    var doubleRedNegativeStyle = isBold || rowType == RowType.SubTotal ? this.DoubleRedNegativeBold : this.DoubleRedNegative;
                    return doubleRedNegativeStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.DoubleRedParenthesisNegative:
                    var doubleRedparenthesisNegativeStyle = isBold || rowType == RowType.SubTotal ? this.DoubleRedparenthesisNegativeBold : this.DoubleRedparenthesisNegative;
                    return doubleRedparenthesisNegativeStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.DoubleParenthesisNegative:
                    var doubleParenthesisNegativeStyle = isBold || rowType == RowType.SubTotal ? this.DoubleParenthesisNegativeBold : this.DoubleParenthesisNegative;
                    return doubleParenthesisNegativeStyle.GetCellStyle(rowType, horizonAlign);

                default:
                    return null;
            }
        }

        private ICellStyle GetPercentageCellStyle(HAlignment horizonAlign, Enums.ExcelCellType cellType, RowType rowType, bool isBold)
        {
            switch (cellType)
            {
                case Enums.ExcelCellType.Percentage:
                    var percentageStyle = isBold || rowType == RowType.SubTotal ? this.PercentageBold : this.Percentage;
                    return percentageStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.PercentageRedPositive:
                    var percentageRedPositiveStyle = isBold || rowType == RowType.SubTotal ? this.PercentageRedPositiveBold : this.PercentageRedPositive;
                    return percentageRedPositiveStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.PercentageRedNegative:
                    var percentageRedNegativeStyle = isBold || rowType == RowType.SubTotal ? this.PercentageRedNegativeBold : this.PercentageRedNegative;
                    return percentageRedNegativeStyle.GetCellStyle(rowType, horizonAlign);
                case Enums.ExcelCellType.PercentageRedparenthesisNegative:
                    var percentageRedparenthesisNegativeStyle = isBold || rowType == RowType.SubTotal ?
                        this.PercentageRedparenthesisNegativeBold :
                        this.PercentageRedparenthesisNegative;
                    return percentageRedparenthesisNegativeStyle.GetCellStyle(rowType, horizonAlign);
                default:
                    return null;
            }
        }

        private ICellStyle GetDateTimeCellStyle(HAlignment horizonAlign, RowType rowType, bool isBold)
        {
            var dateTimeStyle = isBold || rowType == RowType.SubTotal ? this.DateTimeStyleBold : this.DateTimeStyle;
            return dateTimeStyle.GetCellStyle(rowType, horizonAlign);
        }

        private void CreateFont()
        {
            this._bodyFont = this.Workbook.CreateFont();
            this._bodyFont.FontName = "Tahoma";
            this._bodyFont.FontHeightInPoints = 8;
            this._bodyFont.Color = HSSFColor.Black.Index;
        }

        private void CreateBoldFont()
        {
            this._bodyBoldFont = this.Workbook.CreateFont();
            this._bodyBoldFont.FontName = "Tahoma";
            this._bodyBoldFont.FontHeightInPoints = 8;
            this._bodyBoldFont.Color = HSSFColor.Black.Index;
            this._bodyBoldFont.IsBold = true;;
            
        }
    }
}
