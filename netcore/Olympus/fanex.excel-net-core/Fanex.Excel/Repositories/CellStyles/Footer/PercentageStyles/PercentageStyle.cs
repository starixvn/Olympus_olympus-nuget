// -----------------------------------------------------------------------
// <copyright file="PercentageStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer.PercentageStyles
{
    using Body.PercentageStyles.RowTypeStyles;
    using NPOI.SS.UserModel;
    using RowTypeStyles;

    public class PercentageStyle : FooterRowBaseStyle
    {
        public PercentageStyle(IWorkbook workbook, IFont font)
            : base(workbook, font)
        {
            this.Upper = new UpperStyle(workbook, font);
            this.Lower = new LowerStyle(workbook, font);
        }
    }
}
