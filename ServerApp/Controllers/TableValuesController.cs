using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerApp.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json;
using ServerApp.Models.BindingTargets;
using System.IO;

namespace ServerApp.Controllers
{
    [Route("api/excel_rows")]
    public class TableValuesController : Controller
    {
        private DataContext context;
        public TableValuesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public Table GetTable(long id)
        {
            if (!context.Excels.Any(e => e.ExcelId == id))
                return null;
            var excel = context.Excels.First(e => e.ExcelId == id);
            var path = $"{excel.Path}\\{excel.Name}";
            var rows = ExcelReader.GetInvalidRows(path);
            var header = ExcelReader.GetHeaders(path);
            return new Table
            {
                ExcelId = excel.ExcelId,
                Header = header,
                InvalidRows = rows
            };
        }
    }
}
