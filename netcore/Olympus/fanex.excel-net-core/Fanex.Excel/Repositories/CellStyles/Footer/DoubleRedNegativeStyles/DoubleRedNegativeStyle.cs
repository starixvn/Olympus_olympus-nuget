// -----------------------------------------------------------------------
// <copyright file="DoubleRedNegativeStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.DoubleRedNegativeStyles
{
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class DoubleRedNegativeStyle : FooterRowBaseStyle
    {
        public DoubleRedNegativeStyle(IWorkbook workbook, IFont font)
             : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
