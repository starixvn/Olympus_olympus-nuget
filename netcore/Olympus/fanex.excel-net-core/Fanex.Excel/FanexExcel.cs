
namespace Fanex.Excel
{
    using Fanex.Excel.Constants;
    using Fanex.Excel.Enums;
    using Fanex.Excel.Extensions;
    using Fanex.Excel.Models;
    using Fanex.Excel.Repositories.CellStyles;
    using Fanex.Excel.Repositories.CustomColors;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using NPOI.XSSF.UserModel;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    
    using System.IO;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    public class FanexExcel : IFanexExcel
    {
        private const string UnknownSheetName = "Sheet1";

        private IWorkbook _workbook;

        private ISheet _workSheet;

        private GridOptions _gridOptions;

        private List<GridOptions> _listGridOptions;

        private int _startCol = 0;

        private int _startRow = 0;

        private bool _isFreezePane = false;

        private int _currentBodyRowIndex = 0;

        private HeaderCellStyles _headerStyles;

        private BodyCellStyles _bodyStyles;

        private FooterCellStyles _footerStyles;

        private int _columnCount;

        private int _visibleColumnCount;

        private bool _isUseCustomColor;

        public FanexExcel(GridOptions gridOptions, ExcelColor customColor = null)
        {
            this._listGridOptions = new List<GridOptions>();
            this._listGridOptions.Add(gridOptions);
            this._workbook = new HSSFWorkbook();
            this.CustomColor = customColor;
            this._isUseCustomColor = customColor != null;
            this.RegisterCustomColor();
        }

        public FanexExcel(List<GridOptions> listGridOptions, ExcelColor customColor = null)
        {
            this._listGridOptions = listGridOptions;
            this._workbook = new HSSFWorkbook();
            this.CustomColor = customColor;
            this._isUseCustomColor = customColor != null;
            this.RegisterCustomColor();
        }

        public ExcelColor CustomColor { get; set; }

        public HeaderCellStyles HeaderCellStyles
        {
            get
            {
                return this._headerStyles;
            }
        }

        public BodyCellStyles BodyCellStyles
        {
            get
            {
                return this._bodyStyles;
            }
        }

        public FooterCellStyles FooterCellStyles
        {
            get
            {
                return this._footerStyles;
            }
        }

        public IWorkbook WorkBook
        {
            get
            {
                return this._workbook;
            }
        }

        public ISheet Sheet
        {
            get
            {
                return this._workSheet;
            }
        }

        public void ToExcel2010()
        {
            this.ToExcel2010(UnknownSheetName);
        }

        public void ToExcel2010(string sheetName)
        {
            this._workbook = new XSSFWorkbook();
            this._workSheet = this._workbook.CreateSheet(sheetName);
            this._footerStyles = new FooterCellStyles(this._workbook);
            this._headerStyles = new HeaderCellStyles(this._workbook);
            this._bodyStyles = new BodyCellStyles(this._workbook);
            this.RegisterCustomColor();
            this.GenerateExcel();
        }

        public IWorkbook ToExcel2003(string sheetName)
        {
            this._workSheet = this._workbook.CreateSheet(sheetName);
            this._footerStyles = new FooterCellStyles(this._workbook);
            this._headerStyles = new HeaderCellStyles(this._workbook);
            this._bodyStyles = new BodyCellStyles(this._workbook);
            foreach (GridOptions gridOptions in this._listGridOptions)
            {
                this._gridOptions = gridOptions;
                this.RegisterCustomHSSFCellStyles();
                this.DetectHiddenColumn();
                this._columnCount = this._gridOptions.Columns.Count;
                this._visibleColumnCount = this._gridOptions.Columns.Count - this._gridOptions.HiddenColumns.Count;
                this.GenerateExcel();
                this._startRow++;
            }

            return this._workbook;
        }

        public IWorkbook ToExcel2003()
        {
            return this.ToExcel2003(UnknownSheetName);
        }

        public void Write(Stream outputStream)
        {
            this._workbook.Write(outputStream);
        }

        public void Write(FileStream fileStream)
        {
            this._workbook.Write(fileStream);
        }

        public void SetColorAtIndex(short index, byte[] triplet)
        {
            var palette = ((HSSFWorkbook)this._workbook).GetCustomPalette();
            palette.SetColorAtIndex(index, (byte)triplet[0], (byte)triplet[1], (byte)triplet[2]);
        }

        protected virtual void RenderHeader()
        {
            if (this._gridOptions.CustomizeHeader)
            {
                RenderGroupHeader();
            }

            RenderHeaderRow();
        }

        protected void RenderGroupHeader()
        {
            var columnCount = this._gridOptions.GroupHeaderRows.Count;
            for (int i = 0; i < columnCount; i++)
            {
                var headerDataRow = this._gridOptions.GroupHeaderRows[i];
                var headerDataColumnCount = headerDataRow.Columns.Count;
                var headerRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
                headerRow.Height = (short)this._gridOptions.HeaderColumnHeightExcel;
                int cellColIndex = this._startCol;
                for (int j = 0; j < headerDataColumnCount; j++)
                {
                    if (this._gridOptions.Columns[j].HiddenColumn)
                    {
                        continue;
                    }

                    ICell headerCell;
                    ICellStyle headerCellStyle;
                    do
                    {
                        headerCell = this.CreateOrGetCell(headerRow, cellColIndex++);
                        if (headerDataRow.Columns[j].CustomExcelCellStyleIndex >= 0)
                        {
                            headerCellStyle = this.GetCustomHSSFCellStyles(headerDataRow.Columns[j].CustomExcelCellStyleIndex);
                        }
                        else
                        {
                            headerCellStyle = this._headerStyles.GetHeaderStyle(headerDataRow.Columns[j].HeaderAlign);
                        }

                        headerCell.CellStyle = headerCellStyle;
                    } while (headerCell.IsMergedCell);

                    this.SetCellValue(
                        headerCell,
                        headerDataRow.Columns[j].DisplayValue != null ? headerDataRow.Columns[j].DisplayValue : headerDataRow.Columns[j].HeaderText);
                    if (headerDataRow.Columns[j].MergeColNumber != 0 && headerDataRow.Columns[j].MergeRowNumber == 0)
                    {
                        RenderMergeRegionForMergeColumnOnly(headerDataRow, headerRow, j, headerCell, headerCellStyle);

                        cellColIndex = headerCell.ColumnIndex + headerDataRow.Columns[j].MergeColNumber;
                    }

                    if (headerDataRow.Columns[j].MergeRowNumber != 0 && headerDataRow.Columns[j].MergeColNumber == 0)
                    {
                        RenderMergeRegionForMergeRowOnly(headerDataRow, j, headerCell, headerCellStyle);
                    }

                    if (headerDataRow.Columns[j].MergeRowNumber != 0 && headerDataRow.Columns[j].MergeColNumber != 0)
                    {
                        RenderMergeRegionForMergeRowAndColumn(headerDataRow, j, headerCell, headerCellStyle);

                        cellColIndex = headerCell.ColumnIndex + headerDataRow.Columns[j].MergeColNumber;
                    }
                }
            }
        }

        protected void RenderMergeRegionForMergeRowAndColumn(GroupHeaderRows headerDataRow, int dataColIndex, ICell headerCell, ICellStyle headerCellStyle)
        {
            this._workSheet.AddMergedRegion(new CellRangeAddress(
                   headerCell.RowIndex,
                   headerCell.RowIndex + headerDataRow.Columns[dataColIndex].MergeRowNumber - 1,
                   headerCell.ColumnIndex,
                   headerCell.ColumnIndex + headerDataRow.Columns[dataColIndex].MergeColNumber - 1));

            int currentMergeColIndex = headerCell.ColumnIndex;
            int endMergeColIndex = currentMergeColIndex + headerDataRow.Columns[dataColIndex].MergeColNumber;
            int currentMergeRowIndex = headerCell.RowIndex;
            int endMergeRowIndex = currentMergeRowIndex + headerDataRow.Columns[dataColIndex].MergeRowNumber;
            for (int k = currentMergeRowIndex; k < endMergeRowIndex; k++)
            {
                var currentGroupHeaderRow = this.CreateOrGetRow(this._workSheet, k);
                for (int l = currentMergeColIndex; l < endMergeColIndex; l++)
                {
                    this.CreateOrGetCell(currentGroupHeaderRow, l).CellStyle = headerCellStyle;
                }
            }
        }

        protected void RenderMergeRegionForMergeRowOnly(GroupHeaderRows headerDataRow, int dataColIndex, ICell headerCell, ICellStyle headerCellStyle)
        {
            this._workSheet.AddMergedRegion(new CellRangeAddress(
                headerCell.RowIndex,
                headerCell.RowIndex + headerDataRow.Columns[dataColIndex].MergeRowNumber - 1,
                headerCell.ColumnIndex,
                headerCell.ColumnIndex));
            int currentMergeRowIndex = headerCell.RowIndex;
            int endMergeRowIndex = headerCell.RowIndex + headerDataRow.Columns[dataColIndex].MergeRowNumber;
            for (int k = currentMergeRowIndex; k < endMergeRowIndex; k++)
            {
                this.CreateOrGetCell(this.CreateOrGetRow(this._workSheet, k), headerCell.ColumnIndex).CellStyle = headerCellStyle;
            }
        }

        protected void RenderMergeRegionForMergeColumnOnly(
            GroupHeaderRows headerDataRow,
            IRow headerRow,
            int dataColIndex,
            ICell headerCell,
            ICellStyle headerCellStyle)
        {
            this._workSheet.AddMergedRegion(new CellRangeAddress(
                headerCell.RowIndex,
                headerCell.RowIndex,
                headerCell.ColumnIndex,
                headerCell.ColumnIndex + headerDataRow.Columns[dataColIndex].MergeColNumber - 1));
            int currentColIndex = headerCell.ColumnIndex;
            int endMergeColIndex = currentColIndex + headerDataRow.Columns[dataColIndex].MergeColNumber;
            for (int k = currentColIndex + 1; k < endMergeColIndex; k++)
            {
                this.CreateOrGetCell(headerRow, k).CellStyle = headerCellStyle;
            }
        }

        protected virtual void RenderHeaderRow()
        {
            var currentRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
            currentRow.Height = (short)this._gridOptions.HeaderColumnHeightExcel;
            int col = this._startCol;
            for (int i = this._startCol; i < this._columnCount; i++)
            {
                var headerCell = this._gridOptions.Columns[i];
                if (!headerCell.HiddenColumn)
                {
                    ICellStyle headerCellStyle;
                    if (headerCell.CustomExcelCellStyleIndex >= 0)
                    {
                        headerCellStyle = this.GetCustomHSSFCellStyles(headerCell.CustomExcelCellStyleIndex);
                    }
                    else
                    {
                        headerCellStyle = this._headerStyles.GetHeaderStyle(headerCell.HeaderAlign);
                    }

                    var currentCell = this.CreateOrGetCell(currentRow, col);
                    currentCell.CellStyle = headerCellStyle;
                    this.SetCellValue(currentCell, headerCell.DisplayValue != null ? headerCell.DisplayValue : headerCell.HeaderText);
                    this._workSheet.SetColumnWidth(col, headerCell.WidthExcel);
                    if (headerCell.MergeHeaderColNumber > 1)
                    {
                        this._workSheet.AddMergedRegion(new CellRangeAddress(currentCell.RowIndex, currentCell.RowIndex, currentCell.ColumnIndex, currentCell.ColumnIndex + headerCell.MergeHeaderColNumber - 1));
                    }

                    col++;
                }
            }

            if (!this._isFreezePane)
            {
                this._workSheet.CreateFreezePane(0, this._startRow);
                this._isFreezePane = !this._isFreezePane;
            }
        }

        protected virtual void RenderBodyCell(IRow currentRow, ICell currentCell, Columns cell, RowType rowType)
        {
            var header = cell.ReferenceHeaderColumn() ?? this._gridOptions.GetHeaderColumnByName(cell.Name);
            if (this._gridOptions.ShowDistinctRow && (rowType == RowType.Normal && this._currentBodyRowIndex % 2 != 0))
            {
                rowType = RowType.Even;
            }

            ICellStyle cellStyle;
            if (cell.CustomExcelCellStyleIndex >= 0)
            {
                cellStyle = this._gridOptions.CustomExcelHSSFCellStyles[cell.CustomExcelCellStyleIndex];
            }
            else
            {
                cellStyle = this._bodyStyles.GetCellStyle(
                cell.CustomCellType != ExcelCellType.Null ? cell.CustomCellType : header.CustomCellType,
                cell.CellAlignment != Enums.HAlignment.Null ? cell.CellAlignment : header.CellAlignment,
                rowType,
                cell.Bold ? cell.Bold : header.IsBold);
                cellStyle.WrapText = header.WrapText;
            }

            if (cell.Indention != 0)
            {
                var cloneCellStyle = this.WorkBook.CreateCellStyle();
                cloneCellStyle.CloneStyleFrom(cellStyle);
                cloneCellStyle.Indention = cell.Indention;
                currentCell.CellStyle = cloneCellStyle;
            }
            else
            {
                currentCell.CellStyle = cellStyle;
            }

            this.SetCellValue(currentCell, cell.DisplayValue != null ? cell.DisplayValue : cell.Value);
            if (cell.MergeRowNumber > 1)
            {
                RenderMergeRowBodyCell(currentCell, cell, cellStyle);
            }

            if (cell.MergeColNumber > 1)
            {
                RenderMergeColBodyCell(currentRow, currentCell, cell, cellStyle);
            }
        }

        protected void RenderMergeColBodyCell(IRow currentRow, ICell currentCell, Columns cell, ICellStyle cellStyle)
        {
            this._workSheet.AddMergedRegion(new CellRangeAddress(
                currentCell.RowIndex,
                currentCell.RowIndex,
                currentCell.ColumnIndex,
                currentCell.ColumnIndex + cell.MergeColNumber - 1));
            for (int i = 1; i <= cell.MergeColNumber - 1; i++)
            {
                var mergedCell = this.CreateOrGetCell(currentRow, currentCell.ColumnIndex + i);
                mergedCell.CellStyle = cellStyle;
            }
        }

        protected void RenderMergeRowBodyCell(ICell currentCell, Columns cell, ICellStyle cellStyle)
        {
            this._workSheet.AddMergedRegion(new CellRangeAddress(
                currentCell.RowIndex,
                currentCell.RowIndex + cell.MergeRowNumber - 1,
                currentCell.ColumnIndex,
                currentCell.ColumnIndex));
            int currentMergeRowIndex = currentCell.RowIndex;
            int endMergeRowIndex = currentCell.RowIndex + cell.MergeRowNumber;
            for (int i = currentMergeRowIndex + 1; i < endMergeRowIndex; i++)
            {
                var mergeCell = this.CreateOrGetCell(this.CreateOrGetRow(this._workSheet, i), currentCell.ColumnIndex);
                mergeCell.CellStyle = cellStyle;
            }
        }

        protected virtual void RenderFooterRows()
        {
            if (this._gridOptions.ShowFooter && this._gridOptions.FooterRows != null && this._gridOptions.FooterRows.Count > 0)
            {
                int footerColumnCount = this._gridOptions.FooterRows.Count;
                for (int i = 0; i < footerColumnCount; i++)
                {
                    var footerDataRow = this._gridOptions.FooterRows[i];
                    var footerExcelRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
                    footerExcelRow.Height = (short)this._gridOptions.BodyRowHeightExcel;
                    this.RenderFooterRow(footerExcelRow, footerDataRow, this._gridOptions.ShowFooterLineBreak && i == 0);
                }
            }

        }

        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void RenderFooterRow(IRow footerRow, GridDataRow footerDataRow, bool isFirstFooterRow)
        {
            int mergeColNumber = 0;
            int colIndex = this._startCol;
            int footerColumnCount = footerDataRow.Columns.Count;
            for (int i = 0; i < footerColumnCount; i++)
            {
                var footerDataCell = footerDataRow.Columns[i];
                var header = this._gridOptions.Columns[footerDataCell.ColumnIndex];
                var footerCell = this.CreateOrGetCell(footerRow, colIndex++);

                ICellStyle cellStyle;

                if (footerDataCell.CustomExcelCellStyleIndex >= 0)
                {
                    if (isFirstFooterRow)
                    {
                        cellStyle = this.WorkBook.CreateCellStyle();
                        cellStyle.CloneStyleFrom(this._gridOptions.CustomExcelHSSFCellStyles[footerDataCell.CustomExcelCellStyleIndex]);
                        cellStyle.BorderTop = BorderStyle.Thin;
                        cellStyle.TopBorderColor = HSSFColor.Black.Index;
                    }
                    else
                    {
                        cellStyle = this._gridOptions.CustomExcelHSSFCellStyles[footerDataCell.CustomExcelCellStyleIndex];
                    }
                }
                else
                {
                    cellStyle = this._footerStyles.GetCellStyle(
                    footerDataCell.CustomCellType != Enums.ExcelCellType.Null ? footerDataCell.CustomCellType : header.CustomCellType,
                    footerDataCell.CellAlignment == Enums.HAlignment.Null ? header.CellAlignment : footerDataCell.CellAlignment,
                    isFirstFooterRow);
                }

                if (footerDataCell.Indention != 0)
                {
                    var cloneCellStyle = this.WorkBook.CreateCellStyle();
                    cloneCellStyle.CloneStyleFrom(cellStyle);
                    cloneCellStyle.Indention = footerDataCell.Indention;
                    footerCell.CellStyle = cloneCellStyle;
                }
                else
                {
                    footerCell.CellStyle = cellStyle;
                }

                if (header.HiddenColumn && footerDataCell.MergeColNumber > 1)
                {
                    footerDataCell.MergeColNumber -= 1;
                }

                if (mergeColNumber > 0)
                {
                    mergeColNumber--;
                    i--;
                    continue;
                }

                if (footerCell.IsMergedCell())
                {
                    i--;
                    continue;
                }

                this.SetCellValue(footerCell, footerDataCell.DisplayValue != null ? footerDataCell.DisplayValue : footerDataCell.Value);
                mergeColNumber = footerDataCell.MergeColNumber != 0 ? footerDataCell.MergeColNumber - 1 : 0;
                if (footerDataCell.MergeColNumber != 0)
                {
                    this._workSheet.AddMergedRegion(new CellRangeAddress(
                        footerCell.RowIndex,
                        footerCell.RowIndex,
                        footerCell.ColumnIndex,
                        footerCell.ColumnIndex + footerDataCell.MergeColNumber - 1));
                }

                if (footerDataCell.MergeRowNumber != 0)
                {
                    this._workSheet.AddMergedRegion(new CellRangeAddress(
                        footerCell.RowIndex,
                        footerCell.RowIndex + footerDataCell.MergeRowNumber - 1,
                        footerCell.ColumnIndex,
                        footerCell.ColumnIndex));
                }
            }

            while (mergeColNumber > 0)
            {
                var footerCell = this.CreateOrGetCell(footerRow, colIndex++);
                footerCell.CellStyle = this._footerStyles.GetCellStyle(Enums.ExcelCellType.General, Enums.HAlignment.Left, false);
                mergeColNumber--;
            }

            if (footerDataRow.SubRows != null && footerDataRow.SubRows.Count > 0)
            {
                var subFooterRowCount = footerDataRow.SubRows.Count;
                for (int i = 0; i < subFooterRowCount; i++)
                {
                    var footerExcelRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
                    this.RenderFooterRow(footerExcelRow, footerDataRow.SubRows[i], false);
                }
            }
        }

        private void DetectHiddenColumn()
        {
            if (this._gridOptions.HiddenColumns != null && this._gridOptions.HiddenColumns.Count > 0)
            {
                var columnCount = this._gridOptions.Columns.Count;
                for (int i = 0; i < columnCount; i++)
                {
                    if (this._gridOptions.HiddenColumns.Contains(i))
                    {
                        this._gridOptions.Columns[i].HiddenColumn = true;
                    }
                }
            }
        }

        private void RegisterCustomHSSFCellStyles()
        {
            if (this._gridOptions.CustomExcelCellStyles != null)
            {
                this._gridOptions.CustomExcelHSSFCellStyles = new List<ICellStyle>();
                for (int i = 0; i < this._gridOptions.CustomExcelCellStyles.Count; i++)
                {
                    this._gridOptions.CustomExcelHSSFCellStyles.Add(
                        this._gridOptions.CustomExcelCellStyles[i].ToNPOICellStyle(this.WorkBook));
                }
            }
        }

        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "OK")]
        private void RegisterCustomColor()
        {
            if (this._workbook.GetType().Equals(typeof(HSSFWorkbook)))
            {
                this.SetColorAtIndex(TitleColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.TitleBackgroundColor, TitleColor.Triplet) : TitleColor.Triplet);
                this.SetColorAtIndex(HeaderColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.HeaderBackgroundColor, HeaderColor.Triplet) : HeaderColor.Triplet);
                this.SetColorAtIndex(EvenRowColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.BodyEvenBackGroundColor, EvenRowColor.Triplet) : EvenRowColor.Triplet);
                this.SetColorAtIndex(SubTotalRowColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.BodySubTotalBackGroundColor, SubTotalRowColor.Triplet) : SubTotalRowColor.Triplet);
                this.SetColorAtIndex(GrandTotalRowColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.FooterBackgroundColor, GrandTotalRowColor.Triplet) : GrandTotalRowColor.Triplet);
                this.SetColorAtIndex(ParentLevelOneColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.BodyParentLevelOneBackGroundColor, ParentLevelOneColor.Triplet) : ParentLevelOneColor.Triplet);
                this.SetColorAtIndex(ParentLevelTwoColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.BodyParentLevelTwoBackGroundColor, ParentLevelTwoColor.Triplet) : ParentLevelTwoColor.Triplet);
                this.SetColorAtIndex(ParentLevelThreeColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.BodyParentLevelThreeBackGroundColor, ParentLevelThreeColor.Triplet) : ParentLevelThreeColor.Triplet);
                this.SetColorAtIndex(ParentLevelFourColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.BodyParentLevelFourBackGroundColor, ParentLevelFourColor.Triplet) : ParentLevelFourColor.Triplet);
                this.SetColorAtIndex(ParentLevelFiveColor.Index, this._isUseCustomColor ? this.GetColorTriplet(this.CustomColor.BodyParentLevelFiveBackGroundColor, ParentLevelFiveColor.Triplet) : ParentLevelFiveColor.Triplet);
                this.SetColorAtIndex(RevenueLicenseeColor.Index, RevenueLicenseeColor.Triplet);
                this.SetColorAtIndex(NoteInformationColor.Index, NoteInformationColor.Triplet);
            }

            if (this._isUseCustomColor)
            {
                ExcelConstants.TitleBackgroundColor = this.GetColor(this.CustomColor.TitleBackgroundColor, ExcelConstants.TitleBackgroundColor);
                ExcelConstants.HeaderBackgroundColor = this.GetColor(this.CustomColor.HeaderBackgroundColor, ExcelConstants.HeaderBackgroundColor);
                ExcelConstants.FooterBackgroundColor = this.GetColor(this.CustomColor.FooterBackgroundColor, ExcelConstants.FooterBackgroundColor);
                ExcelConstants.EvenRowColor = this.GetColor(this.CustomColor.BodyEvenBackGroundColor, ExcelConstants.EvenRowColor);
                ExcelConstants.SubTotalRowColor = this.GetColor(this.CustomColor.BodySubTotalBackGroundColor, ExcelConstants.SubTotalRowColor);
                ExcelConstants.ParentRowLevelOneColor = this.GetColor(this.CustomColor.BodyParentLevelOneBackGroundColor, ExcelConstants.ParentRowLevelOneColor);
                ExcelConstants.ParentRowLevelTwoColor = this.GetColor(this.CustomColor.BodyParentLevelTwoBackGroundColor, ExcelConstants.ParentRowLevelTwoColor);
                ExcelConstants.ParentRowLevelThreeColor = this.GetColor(this.CustomColor.BodyParentLevelThreeBackGroundColor, ExcelConstants.ParentRowLevelThreeColor);
                ExcelConstants.ParentRowLevelFourColor = this.GetColor(this.CustomColor.BodyParentLevelFourBackGroundColor, ExcelConstants.ParentRowLevelFourColor);
                ExcelConstants.ParentRowLevelFiveColor = this.GetColor(this.CustomColor.BodyParentLevelFiveBackGroundColor, ExcelConstants.ParentRowLevelFiveColor);
            }
        }

        private byte[] GetColorTriplet(Color color, byte[] defaultTriplet)
        {
            if (color == Color.Transparent)
            {
                return defaultTriplet;
            }

            var rgb = color.ToPixel<Rgb24>();
            return new byte[] { rgb.R, rgb.G, rgb.B };
        }

        private Color GetColor(Color color, Color defaultColor)
        {
            return color == Color.Transparent ? defaultColor : color;
        }

        private void GenerateExcel()
        {
            if (this._gridOptions.ShowTitle)
            {
                this.RenderTitleRow();
            }

            this.RenderHeader();
            this.RenderFreezePane();
            this.RenderBodyRow();
            if (this._gridOptions.ShowFooter)
            {
                this.RenderFooterRows();
            }
        }

        private void RenderTitleRow()
        {
            switch (this._gridOptions.TitleType)
            {
                case Enums.TitleType.TwoLineBlue:
                    this.RenderTwoLineBlueTitleRow();
                    break;
                case Enums.TitleType.OneLineBlue:
                    this.RenderOneLineBlueTitleRow();
                    break;
                case Enums.TitleType.TwoLineBlueAndSub:
                    this.RenderTwoLineBlueAndSubTitleRow();
                    break;
                default:
                    this.RenderNormalTitleRow();
                    break;
            }

            this._startRow += this._gridOptions.TitleSeperatorRowNum;
        }

        private void RenderNormalTitleRow()
        {
            var currentRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
            int i = this._startCol;
            for (; i < this._visibleColumnCount; i++)
            {
                var currentCell = this.CreateOrGetCell(currentRow, i);
                currentCell.CellStyle = this._headerStyles.TitleLeft;
                currentCell.SetCellValue(this._gridOptions.TitleInfoText);
            }

            this._workSheet.AddMergedRegion(new CellRangeAddress(this._startRow - 1, this._startRow - 1, this._startCol, i - 1));
        }

        private void RenderTwoLineBlueTitleRow()
        {
            int i = this._startCol;
            for (int j = 0; j < 2; j++)
            {
                var currentRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
                for (; i < this._visibleColumnCount; i++)
                {
                    var currentCell = this.CreateOrGetCell(currentRow, i);
                    currentCell.CellStyle = this._headerStyles.TitleCenter;
                    currentCell.SetCellValue(this._gridOptions.TitleInfoText);
                }
            }

            this._workSheet.AddMergedRegion(new CellRangeAddress(this._startRow - 2, this._startRow - 1, this._startCol, i - 1));
        }

        private void RenderTwoLineBlueAndSubTitleRow()
        {
            for (int j = 0; j < 2; j++)
            {
                var currentRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
                for (int i = this._startCol; i < this._columnCount; i++)
                {
                    var currentCell = this.CreateOrGetCell(currentRow, i);
                    currentCell.CellStyle = this._headerStyles.Center;
                    currentCell.SetCellValue(this._gridOptions.TitleInfoText);
                }
            }

            this._workSheet.AddMergedRegion(new CellRangeAddress(this._startRow - 2, this._startRow - 1, this._startCol, this._gridOptions.Columns.Count - 1));

            if (this._gridOptions.SubtitleRowNum == 0)
            {
                this._gridOptions.SubtitleRowNum = 2;
            }

            for (int j = 0; j < this._gridOptions.SubtitleRowNum; j++)
            {
                var currentRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
                for (int i = this._startCol; i < this._columnCount; i++)
                {
                    var currentCell = this.CreateOrGetCell(currentRow, i);
                    currentCell.CellStyle = this._headerStyles.Left;
                    currentCell.CellStyle.WrapText = true;
                    currentCell.SetCellValue(this._gridOptions.SubtitleInfoText);
                }
            }

            this._workSheet.AddMergedRegion(new CellRangeAddress(
                this._startRow - this._gridOptions.SubtitleRowNum,
                this._startRow - 1,
                this._startCol,
                this._gridOptions.Columns.Count - 1));
        }

        private void RenderOneLineBlueTitleRow()
        {
            var currentRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
            int i = this._startCol;
            for (; i < this._visibleColumnCount; i++)
            {
                var currentCell = this.CreateOrGetCell(currentRow, i);
                currentCell.CellStyle = this._headerStyles.TitleLeft;
                currentCell.SetCellValue(this._gridOptions.TitleInfoText);
            }

            this._workSheet.AddMergedRegion(new CellRangeAddress(this._startRow - 1, this._startRow - 1, this._startCol, i - 1));
        }

        private void RenderBodyRow()
        {
            var rowCount = this._gridOptions.BodyRows.Count;
            this._currentBodyRowIndex = 0;
            for (int i = 0; i < rowCount; i++)
            {
                var currentRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
                currentRow.Height = (short)this._gridOptions.BodyRowHeightExcel;
                var currentDataRow = this._gridOptions.BodyRows[i];
                this.RenderRow(currentDataRow, currentRow, this._gridOptions.Columns.Count);
            }
        }

        private void RenderRow(GridDataRow currentDataRow, IRow currentRow, int numberColumns, int parentLevel = -1)
        {
            this._currentBodyRowIndex += currentDataRow.RowType == RowType.Normal || currentDataRow.RowType == RowType.Even ? 1 : 0;
            int col = this._startCol, columnIndex = 0, numberColumnOfCurrentRow = currentDataRow.Columns.Count;
            ICell curentCell;
            Columns currentDataCell;
            for (int i = 0; i < numberColumns; i++)
            {
                if (i < numberColumnOfCurrentRow)
                {
                    currentDataCell = currentDataRow.Columns[i];
                    var refHeader = currentDataCell.ReferenceHeaderColumn() ?? this._gridOptions.GetHeaderColumnByName(currentDataCell.Name);
                    if (refHeader != null && refHeader.HiddenColumn)
                    {
                        if (currentDataCell.MergeColNumber > 1)
                        {
                            curentCell = CreateOrGetCell(currentRow, columnIndex);
                            this.RenderBodyCell(currentRow, curentCell, currentDataCell, currentDataRow.RowType);
                            columnIndex += --currentDataCell.MergeColNumber;
                        }

                        col++;
                        continue;
                    }
                }

                if (col < numberColumnOfCurrentRow)
                {
                    curentCell = CreateOrGetCell(currentRow, columnIndex++);
                    if (curentCell != null && Extensions.Extensions.IsMergedCell(curentCell))
                    {
                        continue;
                    }

                    currentDataCell = currentDataRow.Columns[col++];
                    currentDataCell.MergeColNumber = currentDataCell.MergeColNumber > _visibleColumnCount ? _visibleColumnCount : currentDataCell.MergeColNumber;
                    this.RenderBodyCell(currentRow, curentCell, currentDataCell, currentDataRow.RowType);
                }
            }

            if (currentDataRow.SubRows != null && currentDataRow.SubRows.Count > 0)
            {
                int numberSubRows = currentDataRow.SubRows.Count;
                for (int j = 0; j < numberSubRows; j++)
                {
                    currentRow = this.CreateOrGetRow(this._workSheet, this._startRow++);
                    var subDataRow = currentDataRow.SubRows[j];
                    this.RenderRow(subDataRow, currentRow, this._gridOptions.Columns.Count);
                }
            }
        }

        private void SetCellValue(ICell cell, object value)
        {
            var type = value.GetType();
            if (type.Equals(typeof(bool)))
            {
                cell.SetCellValue((bool)value);
            }
            else if (type.Equals(typeof(int)))
            {
                cell.SetCellValue((int)value);
            }
            else if (type.Equals(typeof(long)))
            {
                cell.SetCellValue((long)value);
            }
            else if (type.Equals(typeof(double)))
            {
                cell.SetCellValue((double)value);
            }
            else if (type.Equals(typeof(DateTime)))
            {
                cell.SetCellValue((DateTime)value);
            }
            else if (type.Equals(typeof(string)))
            {
                cell.SetCellValue((string)value);
            }
            else if (type.Equals(typeof(IRichTextString)))
            {
                cell.SetCellValue((IRichTextString)value);
            }
        }

        private ICell CreateOrGetCell(IRow row, int cellIndex)
        {
            if (row == null)
            {
                return null;
            }

            if (row.GetCell(cellIndex) != null)
            {
                return row.GetCell(cellIndex);
            }

            return row.CreateCell(cellIndex);
        }

        private IRow CreateOrGetRow(ISheet worksheet, int rowIndex)
        {
            if (worksheet == null)
            {
                return null;
            }

            if (worksheet.GetRow(rowIndex) == null)
            {
                return worksheet.CreateRow(rowIndex);
            }
            else
            {
                return worksheet.GetRow(rowIndex);
            }
        }

        private void RenderFreezePane()
        {
            this._workSheet.CreateFreezePane(this._gridOptions.NumberOfFrozenColumn, this._gridOptions.FreezeHeader ? this._startRow : 0);
        }

        private ICellStyle GetDefaultCellStyle()
        {
            return this.WorkBook.GetCellStyleAt(0);
        }

        private ICellStyle GetCustomHSSFCellStyles(int index)
        {
            try
            {
                return this._gridOptions.CustomExcelHSSFCellStyles[index];
            }
            catch
            {
                return this.GetDefaultCellStyle();
            }
        }
    }
}
