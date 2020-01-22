using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerApp.Models;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using ServerApp.Models.BindingTargets;
using System.Net.Http.Headers;

namespace ServerApp.Controllers
{
    [Route("api/download")]
    public class FileValuesController : Controller
    {
        private DataContext context;
        public FileValuesController(DataContext ctx)
        {
            context = ctx;
        }
        [HttpPost]
        public FileResult Download([FromBody] TableData tData)
        {
            Table table = tData.Table;
            var excel = context.Excels.First(e => e.ExcelId == table.ExcelId);
            byte[] fileBytes = ExcelWriter.GetExcel(excel.Path, excel.Name, table.InvalidRows);
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(fileBytes, contentType);
        }
    }
}
