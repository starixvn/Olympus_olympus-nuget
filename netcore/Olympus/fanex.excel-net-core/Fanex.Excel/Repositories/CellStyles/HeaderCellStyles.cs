// -----------------------------------------------------------------------
// <copyright file="HeaderCellStyles.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class HeaderCellStyles : BaseCellStyles, IHeaderCellStyles
    {
        private IFont _headerFont;

        private IFont _titleFont;

        private IFont _titleWhiteBoldFont;

        public HeaderCellStyles(IWorkbook workbook)
            : base(workbook)
        {
            this._headerFont = this.CreateFont();
            this._titleFont = this.CreateTitleFont();
            this._titleWhiteBoldFont = this.CreateTitleWhiteBoldFont();
            this.CreateTitleCenterStyle();
            this.CreateTitleLeftStyle();
            this.CreateTitleRightStyle();
            this.CreateLeftStyle();
            this.CreateRightStyle();
            this.CreateCenterStyle();
        }

        public ICellStyle TitleCenter { get; set; }

        public ICellStyle TitleLeft { get; set; }

        public ICellStyle TitleRight { get; set; }

        public ICellStyle Left { get; set; }

        public ICellStyle Right { get; set; }

        public ICellStyle Center { get; set; }

        /// <summary>
        /// Gets the header style.
        /// </summary>
        /// <param name="alignment">The alignment.</param>
        /// <returns>Cell Style.</returns>
        public ICellStyle GetHeaderStyle(string alignment)
        {
            switch (alignment)
            {
                case "left":
                    return Left;
                case "right":
                    return Right;
                default:
                    return Center;
            }
        }

        private IFont CreateFont()
        {
            var boldFont = this.Workbook.CreateFont();
            boldFont.FontName = "Tahoma";
            boldFont.FontHeightInPoints = 8;
            boldFont.IsBold = true;
            boldFont.Color = HSSFColor.White.Index;

            return boldFont;
        }

        private IFont CreateTitleFont()
        {
            var titleFont = this.Workbook.CreateFont();
            titleFont.FontName = "Arial";
            titleFont.FontHeightInPoints = 10;
            titleFont.Color = HSSFColor.Black.Index;
            return titleFont;
        }

        private IFont CreateTitleWhiteBoldFont()
        {
            var titleWhiteBoldFont = this.Workbook.CreateFont();
            titleWhiteBoldFont.FontName = "Arial";
            titleWhiteBoldFont.FontHeightInPoints = 10;
            titleWhiteBoldFont.IsBold = true;;
            titleWhiteBoldFont.Color = HSSFColor.White.Index;
            return titleWhiteBoldFont;
        }

        private void CreateTitleCenterStyle()
        {
            this.TitleCenter = this.CreateCellStyle(
                string.Empty,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new TitleColor() : (IColor)(new XSSFColor(ExcelConstants.TitleBackgroundColor)),
                this._titleWhiteBoldFont,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);
        }

        private void CreateTitleLeftStyle()
        {
            this.TitleLeft = this.CreateCellStyle(
                string.Empty,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new TitleColor() : (IColor)(new XSSFColor(ExcelConstants.TitleBackgroundColor)),
                this._titleWhiteBoldFont,
                HorizontalAlignment.Left,
                VerticalAlignment.Top);
        }

        private void CreateTitleRightStyle()
        {
            this.TitleRight = this.CreateCellStyle(
                string.Empty,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new TitleColor() : (IColor)(new XSSFColor(ExcelConstants.TitleBackgroundColor)),
                this._titleWhiteBoldFont,
                HorizontalAlignment.Right,
                VerticalAlignment.Top);
        }

        private void CreateLeftStyle()
        {
            this.Left = this.CreateCellStyle(
                string.Empty,
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new HeaderColor() : (IColor)(new XSSFColor(ExcelConstants.HeaderBackgroundColor)),
                this._headerFont,
                HorizontalAlignment.Left,
                VerticalAlignment.Top);
        }

        private void CreateRightStyle()
        {
            this.Right = this.CreateCellStyle(
                string.Empty, 
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new HeaderColor() : (IColor)(new XSSFColor(ExcelConstants.HeaderBackgroundColor)),
                this._headerFont,
                HorizontalAlignment.Right,
                VerticalAlignment.Top);
        }

        private void CreateCenterStyle()
        {
            this.Center = this.CreateCellStyle(
                string.Empty, 
                this.Workbook.GetType().Equals(typeof(HSSFWorkbook)) ? (IColor)new HeaderColor() : (IColor)(new XSSFColor(ExcelConstants.HeaderBackgroundColor)),
                this._headerFont,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);
        }
    }
}
