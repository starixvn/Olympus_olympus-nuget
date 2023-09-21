// -----------------------------------------------------------------------
// <copyright file="PercentageRedparenthesisNegativeStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.PercentageRedparenthesisNegativeStyles
{
    using Body.PercentageRedparenthesisNegativeStyles.RowTypeStyles;
    using NPOI.SS.UserModel;
    using RowTypeStyles;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PercentageRedparenthesisNegativeStyle : FooterRowBaseStyle
    {
        public PercentageRedparenthesisNegativeStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
