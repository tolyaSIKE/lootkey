using Microsoft.EntityFrameworkCore;
using LootkeyAPI.Models;

namespace LootkeyAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
