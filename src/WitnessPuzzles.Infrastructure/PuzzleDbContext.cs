using Microsoft.EntityFrameworkCore;
using WitnessPuzzles.Infrastructure.Entities;

namespace WitnessPuzzles.Infrastructure;

public class PuzzleDbContext : DbContext
{
    public DbSet<PuzzleEntity> Puzzles { get; set; }

    public PuzzleDbContext(DbContextOptions<PuzzleDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PuzzleEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.OwnsMany(e => e.NodeModifiers);
            entity.OwnsMany(e => e.EdgeModifiers);
            entity.OwnsMany(e => e.CellModifiers);
        });
    }
}
