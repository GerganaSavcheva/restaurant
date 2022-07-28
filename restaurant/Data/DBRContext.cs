using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using restaurant.Models;

namespace restaurant.Data
{
    public class DBRContext : IdentityDbContext
    {
        public DbSet<Restaurant> restaurants { get; set; }

        public DbSet<Post> posts { get; set; }
        public DBRContext(DbContextOptions<DBRContext> options) : base(options)
        {

        }
    }
}
