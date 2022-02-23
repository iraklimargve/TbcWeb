using Microsoft.EntityFrameworkCore;

namespace TbcWeb.DataModels
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
    }
}
