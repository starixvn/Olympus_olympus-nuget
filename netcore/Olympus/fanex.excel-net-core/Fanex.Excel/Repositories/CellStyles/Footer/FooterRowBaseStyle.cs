// -----------------------------------------------------------------------
// <copyright file="BodyRowBaseStyle.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles.Footer
{
    using Fanex.Excel.Enums;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FooterRowBaseStyle : BaseCellStyles
    {
        public FooterRowBaseStyle(IWorkbook workbook, IFont font)
            : base(workbook)
        {
        }

        public BaseCellStyles Upper { get; set; }

        public BaseCellStyles Lower { get; set; }

        /// <summary>
        /// Gets the cell style.
        /// </summary>
        /// <param name="rowType">Type of the row.</param>
        /// <param name="align">The align.</param>
        /// <returns>
        /// Cell Style Object.
        /// </returns>
        public ICellStyle GetCellStyle(FooterRowType rowType, HAlignment align = HAlignment.Null)
        {
            switch (rowType)
            {
                case FooterRowType.Upper:
                    return this.Upper.GetCellStyleByHAlign(align);
                default:
                    return this.Lower.GetCellStyleByHAlign(align);
            }
        }
    }
}