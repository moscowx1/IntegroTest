using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace ServerApp.Controllers
{
    [Route("api/excel_Files")]
    [ApiController]
    public class ExcelFilesValuesController : Controller
    {
        private DataContext context;
        public ExcelFilesValuesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Excel> GetExcelFiles()
        {
            return context.Excels;
        }
    }
}
