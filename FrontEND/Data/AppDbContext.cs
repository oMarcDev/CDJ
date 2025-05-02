using FrontEND.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FrontEND.Data
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
