using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Olympus.WebApi.Results
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

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            using (ExcelPackage)
            {
                var bytes = ExcelPackage.GetAsByteArraySafe(password);
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytes)
                };
            }

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Excel.xlsx"
            };

            return Task.FromResult(response);
        }
    }
}
