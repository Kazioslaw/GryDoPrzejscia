using GryDoPrzejscia.Model;
using Microsoft.EntityFrameworkCore;

namespace GryDoPrzejscia.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }
        public DbSet<GameList> GameList { get; set; }
    }
}
