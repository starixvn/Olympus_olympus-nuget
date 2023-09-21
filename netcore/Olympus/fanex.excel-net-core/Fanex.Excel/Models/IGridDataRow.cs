// -----------------------------------------------------------------------
// <copyright file="IGridDataRow.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;
    using Fanex.Excel.Extensions;
    using Newtonsoft.Json;

    /// <summary>
    /// Grid Data Row.
    /// </summary>
    public interface IGridDataRow
    {
        void AddValue(GridDataRow dataRow);

        GridDataRow Clone(bool isCloneSubRows = false);
    }
}
