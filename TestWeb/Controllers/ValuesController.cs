using System.Web.Http;

namespace TestWeb.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public string Item()
        {
            return "ok";
        }
    }
}