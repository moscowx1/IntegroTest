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
        public FileValuesController(DataContext ctx, ILogger<FileValuesController> log)
        {
            context = ctx;
        }
        [HttpPost]
        public FileResult Download([FromBody] TableData tData)
        {
            Table table = tData.Table;

            string name = context.Excels.First(e => e.ExcelId == table.ExcelId).Name;

            string pathToOrigin = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles");
            ExcelWriter.Create(pathToOrigin, name);
            string pathToNew = $"{pathToOrigin}\\tempFiles\\{name}";
            ExcelWriter.SeedExcel(pathToNew, table.InvalidRows);
            var memory = new MemoryStream();
            using (var stream = new FileStream(pathToNew, FileMode.Open))
            {
                stream.CopyToAsync(memory);
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(pathToNew);

            ExcelWriter.Delete(pathToNew);
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(fileBytes, contentType);
        }
    }
}
