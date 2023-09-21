// -----------------------------------------------------------------------
// <copyright file="IFooterCellStyles.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Repositories.CellStyles
{
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IFooterCellStyles
    {
        ICellStyle GetCellStyle(Enums.ExcelCellType cellType, Enums.HAlignment horizonAlign, bool isFirstFooterRow);

        ICellStyle GetGeneralCellStyle(Enums.HAlignment horizonAlign, bool isFirstFooterRow);
    }
}
