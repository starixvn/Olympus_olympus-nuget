
namespace Fanex.Excel.Models
{
    using System;
    using System.Collections.Generic;
    using Fanex.Excel.Extensions;
    using Newtonsoft.Json;

    public interface IGridDataRow
    {
        void AddValue(GridDataRow dataRow);

        GridDataRow Clone(bool isCloneSubRows = false);
    }
}
