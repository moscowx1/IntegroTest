﻿using OfficeOpenXml;
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
        static string GetTextFromCell(int row, int column)
        {
            return ws.Cells[row, column].Text.Trim();
        }
        static ExcelWorksheet ws;
        private const int startBodyRow = 8;
        private const  int columnCount = 11;

        static public byte[] GetExcel(string path, string name, InvalidRow[] rows, int startRow = startBodyRow)
        {
            var fInfo = new FileInfo($"{path}\\{name}");
            if (!fInfo.Exists)
                return null;

            using (var excel = new ExcelPackage(fInfo))
            {
                if (excel.Workbook.Worksheets.Count == 0)
                    return null;
                ws = excel.Workbook.Worksheets.First();
                int row = startRow;
                string checkS = GetTextFromCell(row, 1);
                while (!string.IsNullOrEmpty(checkS))
                {
                    checkS = GetTextFromCell(++row, 1);
                }
                ws.DeleteRow(startRow, row);
                for(int i = 0; i < rows.Length; i++)
                {
                    for (int j = 0; j < rows[i].Cells.Length; j++)
                    {
                        ws.Cells[startRow + i, j + 1].Value = rows[i].Cells[j].Content;
                    }
                }
                    
                return excel.GetAsByteArray();
            }
        }
    }
}
