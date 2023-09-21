// -----------------------------------------------------------------------
// <copyright file="IntegerStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Body.IntegerStyles
{
    using Fanex.Excel.Enums;
    using Fanex.Excel.Repositories.CellStyles.Body.IntegerStyles.RowTypeStyles;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class IntegerStyle : BodyRowBaseStyle
    {
        public IntegerStyle(IWorkbook workbook, IFont font)
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
