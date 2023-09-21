
namespace Fanex.Excel.Extensions
{
    using System;
    using System.Collections.Generic;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;

    public static class Extensions
    {
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
