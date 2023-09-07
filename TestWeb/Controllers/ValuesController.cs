namespace TestWeb.Controllers
{
    using System.Web.Http;

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