// -----------------------------------------------------------------------
// <copyright file="IntegerRedparenthesisNegativeStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.IntegerRedparenthesisNegativeStyles
{
    using IntegerRedparenthesisNegativeStyles.RowTypeStyles;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class IntegerRedparenthesisNegativeStyle : FooterRowBaseStyle
    {
        public IntegerRedparenthesisNegativeStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
