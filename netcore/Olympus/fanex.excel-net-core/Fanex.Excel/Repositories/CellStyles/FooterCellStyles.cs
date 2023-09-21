// -----------------------------------------------------------------------
// <copyright file="FooterCellStyles.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles
{
    using Fanex.Excel.Enums;
    using Fanex.Excel.Repositories.CellStyles.Footer.DateTimeStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedparenthesisNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedPositiveStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.DoubleStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.GeneralStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.IntegerStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedparenthesisNegativeStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedPositiveStyles;
    using Fanex.Excel.Repositories.CellStyles.Footer.PercentageStyles;
    using Footer.IntegerRedNegativeStyles;
    using Footer.IntegerRedparenthesisNegativeStyles;
    using Footer.IntegerRedPositiveStyles;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FooterCellStyles : BaseCellStyles, IFooterCellStyles
    {
        private IFont _footerFont;

        public FooterCellStyles(IWorkbook workbook)
            : base(workbook)
        {
            this.CreateFont();
            this.General = new GeneralStyle(workbook, this._footerFont);
            this.Integer = new IntegerStyle(workbook, this._footerFont);
            this.IntegerRedPositive = new IntegerRedPositiveStyle(workbook, this._footerFont);
            this.IntegerRedNegative = new IntegerRedNegativeStyle(workbook, this._footerFont);
            this.IntegerRedparenthesisNegative = new IntegerRedparenthesisNegativeStyle(workbook, this._footerFont);
            this.Double = new DoubleStyle(workbook, this._footerFont);
            this.DoubleRedPositive = new DoubleRedPositiveStyle(workbook, this._footerFont);
            this.DoubleRedNegative = new DoubleRedNegativeStyle(workbook, this._footerFont);
            this.DoubleRedparenthesisNegative = new DoubleRedparenthesisNegativeStyle(workbook, this._footerFont);
            this.Percentage = new PercentageStyle(workbook, this._footerFont);
            this.PercentageRedPositive = new PercentageRedPositiveStyle(workbook, this._footerFont);
            this.PercentageRedNegative = new PercentageRedNegativeStyle(workbook, this._footerFont);
            this.PercentageRedparenthesisNegative = new PercentageRedparenthesisNegativeStyle(workbook, this._footerFont);
            this.DateTimeStyle = new DateTimeStyle(workbook, this._footerFont);
            this.DoubleParenthesisNegative = new DoubleParenthesisNegativeStyle(workbook, this._footerFont);
        }

        public GeneralStyle General { get; set; }

        public IntegerStyle Integer { get; set; }

        public IntegerRedPositiveStyle IntegerRedPositive { get; set; }

        public IntegerRedNegativeStyle IntegerRedNegative { get; set; }

        public IntegerRedparenthesisNegativeStyle IntegerRedparenthesisNegative { get; set; }

        public DoubleStyle Double { get; set; }

        public DoubleRedPositiveStyle DoubleRedPositive { get; set; }

        public DoubleRedNegativeStyle DoubleRedNegative { get; set; }

        public DoubleRedparenthesisNegativeStyle DoubleRedparenthesisNegative { get; set; }

        public PercentageStyle Percentage { get; set; }

        public PercentageRedPositiveStyle PercentageRedPositive { get; set; }

        public PercentageRedNegativeStyle PercentageRedNegative { get; set; }

        public PercentageRedparenthesisNegativeStyle PercentageRedparenthesisNegative { get; set; }

        public DateTimeStyle DateTimeStyle { get; set; }

        public DoubleParenthesisNegativeStyle DoubleParenthesisNegative { get; set; }

        /// <summary>
        /// Gets the cell style.
        /// </summary>
        /// <param name="cellType">Type of the cell.</param>
        /// <param name="horizonAlign">The horizon align.</param>
        /// <param name="isFirstFooterRow">If set to <c>true</c> [is first footer row].</param>
        /// <returns>Cell style.</returns>
        public ICellStyle GetCellStyle(ExcelCellType cellType, HAlignment horizonAlign, bool isFirstFooterRow)
        {
            switch (cellType)
            {
                case ExcelCellType.Integer:
                case ExcelCellType.IntegerRedPositive:
                case ExcelCellType.IntegerRedNegative:
                case ExcelCellType.IntegerRedparenthesisNegative:
                    return this.GetIntegerCellStyle(horizonAlign, cellType, isFirstFooterRow);

                case ExcelCellType.Double:
                case ExcelCellType.DoubleRedPositive:
                case ExcelCellType.DoubleRedPositiveNoteInformation:
                case ExcelCellType.DoubleRedNegative:
                case ExcelCellType.DoubleRedParenthesisNegative:
                case ExcelCellType.DoubleParenthesisNegative:
                    return this.GetDoubleCellStyle(horizonAlign, cellType, isFirstFooterRow);

                case ExcelCellType.Percentage:
                case ExcelCellType.PercentageRedPositive:
                case ExcelCellType.PercentageRedNegative:
                case ExcelCellType.PercentageRedparenthesisNegative:
                    return this.GetPercentageCellStyle(horizonAlign, cellType, isFirstFooterRow);

                case ExcelCellType.DateTimeMMDDYYYY:
                    return isFirstFooterRow ? this.DateTimeStyle.UpperStyle : this.DateTimeStyle.Style;

                case ExcelCellType.FooterInformation:
                    return this.General.Info.Style;

                default:
                    return this.GetGeneralCellStyle(horizonAlign, isFirstFooterRow);
            }
        }

        /// <summary>
        /// Gets the general cell style.
        /// </summary>
        /// <param name="horizonAlign">The horizon align.</param>
        /// <param name="isFirstFooterRow">If set to <c>true</c> [is first footer row].</param>
        /// <returns>Cell Style.</returns>
        public ICellStyle GetGeneralCellStyle(HAlignment horizonAlign, bool isFirstFooterRow)
        {
            switch (horizonAlign)
            {
                case HAlignment.Left:
                    return isFirstFooterRow ? this.General.Left.UpperStyle : this.General.Left.Style;

                case HAlignment.Right:
                    return isFirstFooterRow ? this.General.Right.UpperStyle : this.General.Right.Style;

                case HAlignment.Center:
                    return isFirstFooterRow ? this.General.Center.UpperStyle : this.General.Center.Style;

                default:
                    return isFirstFooterRow ? this.General.Left.UpperStyle : this.General.Left.Style;
            }
        }

        /// <summary>
        /// Gets the integer cell style.
        /// </summary>
        /// <param name="cellType">Type of the cell.</param>
        /// <param name="isFirstFooterRow">If set to <c>true</c> [is first footer row].</param>
        /// <returns>Integer Cell Style.</returns>
        private ICellStyle GetIntegerCellStyle(HAlignment horizonAlign, Enums.ExcelCellType cellType, bool isFirstFooterRow)
        {
            switch (cellType)
            {
                case Enums.ExcelCellType.Integer:
                    return this.Integer.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign) ;

                case Enums.ExcelCellType.IntegerRedPositive:
                    return this.IntegerRedPositive.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.IntegerRedNegative:
                    return this.IntegerRedNegative.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.IntegerRedparenthesisNegative:
                    return this.IntegerRedparenthesisNegative.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the Double cell style.
        /// </summary>
        /// <param name="cellType">Type of the cell.</param>
        /// <param name="isFirstFooterRow">If set to <c>true</c> [is first footer row].</param>
        /// <returns>Double Cell Style.</returns>
        private ICellStyle GetDoubleCellStyle(HAlignment horizonAlign, Enums.ExcelCellType cellType, bool isFirstFooterRow)
        {
            switch (cellType)
            {
                case Enums.ExcelCellType.Double:
                    return this.Double.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.DoubleRedPositive:
                    return this.DoubleRedPositive.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.DoubleRedPositiveNoteInformation:
                    return this.DoubleRedPositive.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.DoubleRedNegative:
                    return this.DoubleRedNegative.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.DoubleRedParenthesisNegative:
                    return this.DoubleRedparenthesisNegative.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.DoubleParenthesisNegative:
                    return this.DoubleParenthesisNegative.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the Percentage cell style.
        /// </summary>
        /// <param name="cellType">Type of the cell.</param>
        /// <param name="isFirstFooterRow">If set to <c>true</c> [is first footer row].</param>
        /// <returns>Percentage Cell Style.</returns>
        private ICellStyle GetPercentageCellStyle(HAlignment horizonAlign, Enums.ExcelCellType cellType, bool isFirstFooterRow)
        {
            switch (cellType)
            {
                case Enums.ExcelCellType.Percentage:
                    return this.Percentage.GetCellStyle(isFirstFooterRow?FooterRowType.Upper: FooterRowType.Lower, horizonAlign) ;

                case Enums.ExcelCellType.PercentageRedPositive:
                    return this.PercentageRedPositive.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.PercentageRedNegative:
                    return this.PercentageRedNegative.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                case Enums.ExcelCellType.PercentageRedparenthesisNegative:
                    return this.PercentageRedparenthesisNegative.GetCellStyle(isFirstFooterRow ? FooterRowType.Upper : FooterRowType.Lower, horizonAlign);

                default:
                    return null;
            }
        }

        private void CreateFont()
        {
            this._footerFont = this.Workbook.CreateFont();
            this._footerFont.FontName = "Tahoma";
            this._footerFont.FontHeightInPoints = 8;
            this._footerFont.IsBold = true;;
            this._footerFont.Color = HSSFColor.Black.Index;
        }
    }
}