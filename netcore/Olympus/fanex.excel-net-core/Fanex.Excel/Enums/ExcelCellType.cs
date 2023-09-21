
namespace Fanex.Excel.Enums
{
    using System.Diagnostics.CodeAnalysis;

    public enum ExcelCellType
    {
        Null,

        General,

        Integer,

        IntegerParenthesisNegative,

        IntegerRedPositive,

        IntegerRedNegative,

        IntegerRedparenthesisNegative,

        Double,

        DoubleParenthesisNegative,

        DoubleRedPositive,

        DoubleRedPositiveNoteInformation,

        DoubleRedNegative,

        DoubleRedParenthesisNegative,

        Percentage,

        PercentageParenthesisNegative,

        PercentageRedPositive,

        PercentageRedNegative,

        PercentageRedparenthesisNegative,

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "This suppress is OK.")]
        DateTimeMMDDYYYY,

        FooterInformation,

        Exchange
    }
}
