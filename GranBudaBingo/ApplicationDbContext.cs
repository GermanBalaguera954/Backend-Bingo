using GranBudaBingo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GranBudaBingo
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<BingoBall> BingoBalls { get; set; }
        public DbSet<BingoCard> BingoCards { get; set; }
        public DbSet<BingoGame> BingoGames { get; set; }


        // Configure model relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación uno a muchos entre BingoGame y BingoCard
            modelBuilder.Entity<BingoGame>()
                .HasMany(g => g.BingoCards)
                .WithOne(c => c.BingoGame)
                .HasForeignKey(c => c.BingoGameId);

            // Relación uno a muchos entre BingoGame y BingoBall
            modelBuilder.Entity<BingoGame>()
                .HasMany(g => g.DrawnBalls)
                .WithOne(b => b.BingoGame)
                .HasForeignKey(b => b.BingoGameId);
        }
    }
}
