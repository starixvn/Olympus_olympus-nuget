namespace TestWeb.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Data.Repository;
    using Newtonsoft.Json;
    using Criteria;
    using Models;

    public class HomeController : Controller
    {
        private string OutputText = "";
        [Authorize]
        public ActionResult Index()
        {
            var repository = new DynamicRepository();

            var criteria1 = new JacobReturnTableAndOutputCriteria();
            var criteria2 = new JacobReturn2TableAndOutputCriteria();
            var criteria3 = new JacobReturnOutputAndRandomTableCriteria();

            var list = repository.Fetch<Table1>(criteria1).ToList();

            WriteObject("TableAndOutput. Table", list);
            OutputText += $"TableAndOutput. Output: {criteria1.MyOutput}<br/>";

            var tube1 = repository.FetchMultiple<Table1, Table2>(criteria2);
            var list1 = tube1.Item1.ToList();
            var list2 = tube1.Item2.ToList();

            WriteObject("2TableAndOutputCriteria. Table1", list1);
            WriteObject("2TableAndOutputCriteria. Table2", list2);
            OutputText += $"2TableAndOutputCriteria. Output: {criteria2.MyOutput}<br/>";
            OutputText += "==============================================<br/>";

            for (var i = 0; i < 20; i++)
            {
                criteria3.MyOutput = i % 4;

                OutputText += $"==================== Option: {criteria3.MyOutput}=========<br/>";
                var tube2 = repository.FetchMultiple<Table1, Table2, Table2>(criteria3);
                var list3 = tube2.Item1.ToList();
                var list4 = tube2.Item2.ToList();
                var list5 = tube2.Item3.ToList();

                OutputText += $"OutputAndRandomTableCriteria. Output: {criteria3.MyOutput}<br/>";
                WriteObject("OutputAndRandomTableCriteria. Table1", list3);
                WriteObject("OutputAndRandomTableCriteria. Table2", list4);
                WriteObject("OutputAndRandomTableCriteria. Table3", list5);
            }

            ViewBag.Log = OutputText;
            return View();
        }

        private void WriteObject(string name, object o)
        {
            var str = JsonConvert.SerializeObject(o);
            OutputText += $"{name}:{str}<br/>";
        }
    }
}
