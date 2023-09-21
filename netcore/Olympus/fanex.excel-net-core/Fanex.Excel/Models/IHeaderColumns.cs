
namespace Fanex.Excel.Models
{
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
