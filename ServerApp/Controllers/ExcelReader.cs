using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerApp.Controllers
{
    public class ExcelReader
    {
        static string GetTextFromCell(int row, int column)
        {
            return ws.Cells[row, column].Text.Trim();
        }

        static ExcelWorksheet ws;
        private static int startBodyRow = 8;
        private static int columnCount = 11;

        private static List<Cell[]> ExcelRead(string path, SelectRowFunc Func, int startRow, Nullable<int> endRow)
        {
            var rows = new List<Cell[]>();
            var fInfo = new FileInfo(path);
            if (!fInfo.Exists)
                return rows;

            using (var excel = new ExcelPackage(fInfo))
            {
                if (excel.Workbook.Worksheets.Count == 0)
                    return rows;
                ws = excel.Workbook.Worksheets.First();

                int row = startRow;
                string checkS = GetTextFromCell(row, 1);
                while (!string.IsNullOrEmpty(checkS) || row <= endRow && endRow != null)
                {
                    var rowCheck = new string[columnCount];
                    for(int i = 0; i < columnCount; i++)
                    {
                        rowCheck[i] = GetTextFromCell(row, i + 1);
                    }
                    if (Func(rowCheck.ToArray()))
                    {
                        Cell[] tempRows = new Cell[columnCount];
                        
                        rows.Add(rowCheck.Select((r, i) => new Cell
                        {
                            ColumnNumber = i + 1,
                            Content = r
                        }).ToArray());
                    }
                    checkS = GetTextFromCell(++row, 1);
                }
            }
            return rows;
        }

        public static Cell[] GetHeaders(string path)
        {
            return ExcelRead(path, GetAll, 5, 5).First();
        }

        private static bool GetAll(string[] arr)
        {
            return true;
        }

        private static char[] validSym = { 'X', 'Х' };
        private static bool IsEqualToValidSym(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            char sym = char.ToUpper(s[0]);
            foreach (char c in validSym)
                if (sym == c)
                    return true;
            return false;
        }

        private static int[] columnsForCheck = { 1, 2, 3, 4, 5, 6, 11 };

        private static bool IsInvalidRow(string[] data)
        {
            var check = new List<string>();
            foreach (int col in columnsForCheck)
                check.Add(data[col - 1]);

            if (!IsEqualToValidSym(check.Last()))
                return false;

            for (int i = 0; i < check.Count - 1; i++)
            {
                if (IsEqualToValidSym(check[i]))
                    return false;
            }
            return true;
        }

        public static InvalidRow[] GetInvalidRows(string path)
        {
            var listCells = ExcelRead(path, IsInvalidRow, startBodyRow, null);
            var invalidRows = new List<InvalidRow>();
            foreach (var cells in listCells)
            {
                var invalidCells = cells.Select(c => new Cell
                {
                    Content = c.Content,
                    ColumnNumber = c.ColumnNumber
                    
                }).ToArray();
                invalidRows.Add(new InvalidRow
                {
                    Cells = invalidCells,
                    RowNumber = cells.First().ColumnNumber,
                    IsSelected = true
                });
            }
            return invalidRows.ToArray();
        }

        delegate bool SelectRowFunc(string[] array);
    }
}
