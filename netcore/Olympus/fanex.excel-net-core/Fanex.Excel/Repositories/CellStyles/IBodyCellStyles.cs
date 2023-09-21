// -----------------------------------------------------------------------
// <copyright file="IBodyCellStyles.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles
{
    using Fanex.Excel.Enums;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IBodyCellStyles
    {
        ICellStyle GetCellStyle(Enums.ExcelCellType cellType, Enums.HAlignment horizonAlign, RowType rowType, bool isBold = false);

        ICellStyle GetGeneralCellStyle(Enums.HAlignment horizonAlign, RowType rowType, bool isBold = false);
    }
}
