using BackEND.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEND.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
