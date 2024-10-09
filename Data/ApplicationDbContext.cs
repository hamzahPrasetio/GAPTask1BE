using Microsoft.EntityFrameworkCore;
using task1be.Models;

namespace task1be.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships and constraints
            modelBuilder.Entity<Player>()
                .HasOne(p => p.position)
                .WithMany(pos => pos.players)
                .HasForeignKey(p => p.positionid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.team)
                .WithMany(t => t.players)
                .HasForeignKey(p => p.teamid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
