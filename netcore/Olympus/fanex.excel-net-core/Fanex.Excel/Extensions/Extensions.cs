// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Extensions
{
    using System;
    using System.Collections.Generic;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets the is merged cell.
        /// </summary>
        /// <param name="cell">The current cell.</param>
        /// <returns>Is merged cell.</returns>
        public static bool IsMergedCell(this ICell cell)
        {
            for (int i = 0; i < cell.Sheet.NumMergedRegions; i++)
            {
                CellRangeAddress mergedRange = cell.Sheet.GetMergedRegion(i);

                if (cell.ColumnIndex >= mergedRange.FirstColumn && cell.ColumnIndex <= mergedRange.LastColumn
                    && cell.RowIndex >= mergedRange.FirstRow && cell.RowIndex <= mergedRange.LastRow)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Sets the cell value.
        /// </summary>
        /// <param name="cell">The cell of row.</param>
        /// <param name="cellValue">The cell value.</param>
        public static void SetCellValue(ICell cell, object cellValue)
        {
            if (cellValue is string)
            {
                cell.SetCellValue(cellValue.ToString());
            }
            else if (cellValue is int)
            {
                cell.SetCellValue((int)cellValue);
            }
            else if (cellValue is long)
            {
                cell.SetCellValue((long)cellValue);
            }
            else if (cellValue is double)
            {
                cell.SetCellValue((double)cellValue);
            }
            else if (cellValue is decimal)
            {
                cell.SetCellValue(Convert.ToDouble(cellValue));
            }
            else if (cellValue is bool)
            {
                cell.SetCellValue((bool)cellValue);
            }
            else if (cellValue is DateTime)
            {
                cell.SetCellValue((DateTime)cellValue);
            }
        }

        /// <summary>
        /// Determines whether the specified type is numeric.
        /// </summary>
        /// <param name="type">The current type.</param>
        /// <returns>Is numeric.</returns>
        public static bool IsNumeric(Type type)
        {
            if (type == null)
            {
                return false;
            }

            var numericTypes = new List<Type>() 
            { 
                typeof(byte),
                typeof(decimal),
                typeof(double),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(sbyte),
                typeof(float),
                typeof(ushort),
                typeof(uint),
                typeof(ulong)
            };

            return numericTypes.Contains(type);
        }

        /// <summary>
        /// Check the Type is integer or not.
        /// </summary>
        /// <param name="type">The type object.</param>
        /// <returns>
        /// A boolean value describe if DataColumn is integer or not.
        /// </returns>
        public static bool IsInteger(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            var integerTypes = new List<Type>() 
            {
                typeof(byte),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(sbyte),
                typeof(ushort),
                typeof(uint),
                typeof(ulong) 
            };

            return integerTypes.Contains(type);
        }

        /// <summary>
        /// Check the Type is decimal or not.
        /// </summary>
        /// <param name="type">The type object.</param>
        /// <returns>
        /// A boolean value describe if DataColumn is decimal or not.
        /// </returns>
        public static bool IsDecimal(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            var numericTypes = new List<Type>() 
            { 
                typeof(decimal),
                typeof(double),
                typeof(float)
            };

            return numericTypes.Contains(type);
        }
    }
}
