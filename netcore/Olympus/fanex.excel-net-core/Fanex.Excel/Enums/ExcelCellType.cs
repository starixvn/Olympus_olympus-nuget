// -----------------------------------------------------------------------
// <copyright file="ExcelCellType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Excel.Enums
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public enum ExcelCellType
    {
        /// <summary>
        /// The Null value.
        /// </summary>
        Null,

        /// <summary>
        /// The general.
        /// </summary>
        General,

        /// <summary>
        /// The integer.
        /// </summary>
        Integer,

        /// <summary>
        /// The integer parenthesis negative.
        /// </summary>
        IntegerParenthesisNegative,

        /// <summary>
        /// The integer red positive.
        /// </summary>
        IntegerRedPositive,

        /// <summary>
        /// The integer red negative.
        /// </summary>
        IntegerRedNegative,

        /// <summary>
        /// The integer red parenthesis negative.
        /// </summary>
        IntegerRedparenthesisNegative,

        /// <summary>
        /// The double.
        /// </summary>
        Double,

        /// <summary>
        /// The double parenthesis negative.
        /// </summary>
        DoubleParenthesisNegative,

        /// <summary>
        /// The double red positive.
        /// </summary>
        DoubleRedPositive,

        /// <summary>
        /// The double red positive note information.
        /// </summary>
        DoubleRedPositiveNoteInformation,

        /// <summary>
        /// The double red negative.
        /// </summary>
        DoubleRedNegative,

        /// <summary>
        /// The double red parenthesis negative.
        /// </summary>
        DoubleRedParenthesisNegative,

        /// <summary>
        /// The percentage.
        /// </summary>
        Percentage,

        /// <summary>
        /// The percentage parenthesis negative.
        /// </summary>
        PercentageParenthesisNegative,

        /// <summary>
        /// The percentage red positive.
        /// </summary>
        PercentageRedPositive,

        /// <summary>
        /// The percentage red negative.
        /// </summary>
        PercentageRedNegative,

        /// <summary>
        /// The percentage red parenthesis negative.
        /// </summary>
        PercentageRedparenthesisNegative,

        /// <summary>
        /// The date time MMDDYYYY.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "This suppress is OK.")]
        DateTimeMMDDYYYY,

        /// <summary>
        /// The footer information.
        /// </summary>
        FooterInformation,

        /// <summary>
        /// The exchange information.
        /// </summary>
        Exchange
    }
}
