// -----------------------------------------------------------------------
// <copyright file="IFanexExcel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel
{
    using System.IO;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IFanexExcel
    {
        void ToExcel2010();

        void ToExcel2010(string sheetName);

        IWorkbook ToExcel2003();

        IWorkbook ToExcel2003(string sheetName);

        void Write(Stream outputStream);

        void Write(FileStream fileStream);
    }
}