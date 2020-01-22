namespace ServerApp.Models
{
    public class Cell
    {
        public long ColumnNumber { get; set; }
        public string Content { get; set; }
    }
    public class InvalidRow
    {
        public long RowNumber { get; set; }
        public Cell[] Cells { get; set; }
        public bool IsSelected { get; set; }
    }

    public class Table
    {
        public long ExcelId { get; set; }
        public Cell[] Header { get; set; }
        public InvalidRow[] InvalidRows { get; set; }
    }
}
