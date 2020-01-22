using OfficeOpenXml;
using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace ServerApp.Controllers
{
    public class ExcelWriter
    {
        static ILogger<ExcelWriter> logger;
        public ExcelWriter(ILogger<ExcelWriter> log)
        {
            logger = log;
        }
        static string GetTextFromCell(int row, int column)
        {
            return ws.Cells[row, column].Text.Trim();
        }
        static ExcelWorksheet ws;
        private static int startBodyRow = 8;
        private static int columnCount = 11;

        private static void ExcelRemove(string path, int startRow, Nullable<int> endRow)
        {
            var rows = new List<Cell[]>();
            var fInfo = new FileInfo(path);
            if (!fInfo.Exists)
                return;
            using (var excel = new ExcelPackage(fInfo))
            {
                if (excel.Workbook.Worksheets.Count == 0)
                    return;
                ws = excel.Workbook.Worksheets.First();
                int row = startRow;
                string checkS = GetTextFromCell(row, 1);
                while (!string.IsNullOrEmpty(checkS) || row <= endRow && endRow != null)
                {
                    checkS = GetTextFromCell(++row, 1);
                }
                ws.DeleteRow(startBodyRow, row);
                excel.Save();
            }
        }

        private static void ExcelWrite(string path, int startRow, Nullable<int> endRow, InvalidRow[] rows)
        {
            var fInfo = new FileInfo(path);
            if (!fInfo.Exists)
                return;

            using (var excel = new ExcelPackage(fInfo))
            {
                if (excel.Workbook.Worksheets.Count == 0)
                    return;
                ws = excel.Workbook.Worksheets.First();
                int row = startRow;
                for (int i = 0; i < rows.Length; i++)
                {
                    for (int j = 0; j < rows[i].Cells.Length; j++)
                    {
                        ws.Cells[row + i, j + 1].Value = rows[i].Cells[j].Content;
                    }
                }
                excel.Save();
            }
        }
        static public void Create(string path, string name)
        {
            File.Copy($"{path}\\{name}", $"{path}\\tempFiles\\{name}", true);
        }
        static public void Delete(string path)
        {
            File.Delete(path);
        }
        static public void SeedExcel(string path, InvalidRow[] rows)
        {
            ExcelRemove($"{path}", startBodyRow, null);
            ExcelWrite($"{path}", startBodyRow, null, rows);
        }
    }
}
