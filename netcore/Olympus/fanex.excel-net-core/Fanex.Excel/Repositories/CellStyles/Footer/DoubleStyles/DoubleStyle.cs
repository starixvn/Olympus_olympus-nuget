// -----------------------------------------------------------------------
// <copyright file="DoubleStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.DoubleStyles
{
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class DoubleStyle : FooterRowBaseStyle
    {
        public DoubleStyle(IWorkbook workbook, IFont font)
             : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
