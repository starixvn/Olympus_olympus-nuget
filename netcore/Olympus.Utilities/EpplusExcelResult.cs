using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using OfficeOpenXml;

namespace Olympus.Utilities
{
    public class EpplusExcelResult : IHttpActionResult
    {
        private readonly string password;

        public EpplusExcelResult(ExcelPackage excelPackage, string password = "")
        {
            ExcelPackage = excelPackage;
            this.password = password;
        }

        public ExcelPackage ExcelPackage { get; }

        public string FileName { get; set; } = "Excel.xlsx";

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            using (ExcelPackage)
            {
                var bytes = await ExcelPackage.GetAsByteArrayAsync(password);
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytes)
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = FileName
                };

                return await Task.FromResult(response);
            }
        }
    }
}
