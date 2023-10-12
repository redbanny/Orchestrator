using Microsoft.EntityFrameworkCore;
using OrchestratorAPI.Models;

namespace OrchestratorAPI.Contexts
{
    public class TurnDbContext : DbContext
    {
        public TurnDbContext(DbContextOptions<TurnDbContext> options) : base(options) =>
            Database.EnsureCreated();

        public DbSet<Turn> Turns { get; set; }
        public DbSet<TurnItem> TurnItems { get; set; }
    }
}
