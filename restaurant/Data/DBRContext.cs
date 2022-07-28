using Microsoft.EntityFrameworkCore;
using restaurant.Models;

namespace restaurant.Data
{
    public class DBRContext : DbContext
    {
        public DbSet<Restaurant> restaurants { get; set; }

        public DbSet<Post> posts { get; set; }
        public DBRContext(DbContextOptions<DBRContext> options) : base(options)
        {

        }
    }
}
