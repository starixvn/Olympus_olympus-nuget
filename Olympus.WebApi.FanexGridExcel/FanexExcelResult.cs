using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Fanex.Excel;
using Fanex.Excel.Models;
using NPOI.SS.UserModel;

namespace Olympus.WebApi.FanexGridExcel
{
    public class FanexExcelResult : IHttpActionResult
    {
        public FanexExcelResult(GridOptions gridOptions, string sheetName)
        {
            var excel = new FanexExcel(gridOptions ?? new GridOptions());
            Workbook = excel.ToExcel2003(sheetName);
        }

        public FanexExcelResult(List<GridOptions> listGridOptions, string sheetName)
        {
            var excel = new FanexExcel(listGridOptions ?? new List<GridOptions>());
            Workbook = excel.ToExcel2003(sheetName);
        }

        public IWorkbook Workbook { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream())
            {
                Workbook.Write(memoryStream);
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "undefined.xls"
                };

                return Task.FromResult(response);
            }
        }
    }
}
