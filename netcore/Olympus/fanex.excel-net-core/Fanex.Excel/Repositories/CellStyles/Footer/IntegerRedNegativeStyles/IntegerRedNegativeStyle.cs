// -----------------------------------------------------------------------
// <copyright file="IntegerRedNegativeStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.IntegerRedNegativeStyles
{
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class IntegerRedNegativeStyle : FooterRowBaseStyle
    {
        public IntegerRedNegativeStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
