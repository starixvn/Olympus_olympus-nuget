// -----------------------------------------------------------------------
// <copyright file="IHeaderColumns.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Models
{
    /// <summary>
    /// Header Columns.
    /// </summary>
    public interface IHeaderColumns
    {
        bool ShouldSerializeFormatNumber();

        bool ShouldSerializeFormatNegative();

        bool ShouldSerializeFormatPositive();

        bool ShouldSerializeFormatDateTime();

        bool ShouldSerializeTHCssClass();

        bool ShouldSerializeWidth();

        bool ShouldSerializeSort();

        bool ShouldSerializeSortDirection();

        bool ShouldSerializeSortType();

        HeaderColumns Clone();
    }
}
