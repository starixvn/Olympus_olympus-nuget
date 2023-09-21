// -----------------------------------------------------------------------
// <copyright file="PercentageStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Body.PercentageStyles
{
    using Fanex.Excel.Repositories.CellStyles.Body.PercentageStyles.RowTypeStyles;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PercentageStyle : BodyRowBaseStyle
    {
        public PercentageStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Odd = new OddStyle(workbook, font);
            this.Even = new EvenStyle(workbook, font);
            this.SubTotal = new SubTotalStyle(workbook, font);
            this.ParentLevelOne = new ParentLevelOneStyle(workbook, font);
            this.ParentLevelTwo = new ParentLevelTwoStyle(workbook, font);
            this.ParentLevelThree = new ParentLevelThreeStyle(workbook, font);
            this.ParentLevelFour = new ParentLevelFourStyle(workbook, font);
            this.ParentLevelFive = new ParentLevelFiveStyle(workbook, font);
        }
    }
}