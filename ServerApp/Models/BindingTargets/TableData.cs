using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class TableData
    {
        [Required]
        public long ExcelId { get; set; }

        public Cell[] Header { get; set; }

        [Required]
        public InvalidRow[] InvalidRows { get; set; }

        public Table Table => new Table
        {
            ExcelId = ExcelId,
            Header = null,
            InvalidRows = InvalidRows
        };
    }
}