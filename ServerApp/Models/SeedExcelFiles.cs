using Microsoft.EntityFrameworkCore;
using Spire.Xls;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerApp.Models
{
    public class SeedExcelFiles
    {
        private static string pathToExcel = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles");

        private static List<Excel> GetExcels()
        {
            string[] fileNames = Directory.GetFiles(pathToExcel);

            var excels = new List<Excel>();
            foreach (string file in fileNames)
            {
                FileInfo fInfo = new FileInfo(file);
                if (fInfo.Attributes.HasFlag(FileAttributes.Hidden))
                    continue;
                if (fInfo.Extension == ".xls")
                {
                    string name = fInfo.Name;
                    name = name.Substring(0, name.Length - 4);
                    ConvertToXLSX(name);
                    excels.Add(new Excel
                    {
                        Path = pathToExcel,
                        Name = $"{name}.xlsx"
                    });
                }
                if (fInfo.Extension == ".xlsx")
                {
                    excels.Add(new Excel
                    {
                        Path = pathToExcel,
                        Name = fInfo.Name
                    });
                }
            }

            return excels;
        }

        private static void ConvertToXLSX(string name)
        {

            Workbook workBook = new Workbook();
            workBook.LoadFromFile($"{pathToExcel}\\{name}.xls");
            workBook.SaveToFile($"{pathToExcel}\\{name}.xlsx", ExcelVersion.Version2013);
            File.Delete($"{pathToExcel}\\{name}.xls");
        }

        public static void Seed(DataContext context)
        {
            context.Database.Migrate();

            if (context.Excels.Count() == 0)
            {
                var excels = GetExcels();
                context.Excels.AddRange(excels);
            }
            context.SaveChanges();
        }
    }
}
