using Microsoft.EntityFrameworkCore;

namespace ServerApp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<Excel> Excels { get; set; }
    }
}
