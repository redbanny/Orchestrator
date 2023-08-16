using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OrchestratorAPI.Models;

namespace OrchestratorAPI.Contexts
{
    public class TurnDbContext : DbContext
    {
        public TurnDbContext(DbContextOptions<TurnDbContext> options) : base(options) =>
            Database.EnsureCreated();

        public DbSet<Turn> Turns { get; set; }
        public DbSet<TurnItem> TurnItems { get; set; }
        public DbSet<InputDate> InputDate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Turn>()
                .HasMany(t => t.TurnItems)
                .WithOne(t => t.Turn)
                .HasForeignKey(t => t.TurnId).IsRequired();

            /*modelBuilder.Entity<TurnItem>()
                .HasOne(t => t.InputDate)
                .WithOne(t => t.TurnItem)
                .HasForeignKey<InputDate>(t => t.TurnItemId).IsRequired();*/
        }
    }
}
