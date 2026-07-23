using Microsoft.EntityFrameworkCore;
using WitnessPuzzles.Infrastructure.Configurations;
using WitnessPuzzles.Infrastructure.Entities;

namespace WitnessPuzzles.Infrastructure;

public class PuzzleDbContext : DbContext
{
    public DbSet<PuzzleEntity> Puzzles { get; set; }

    public PuzzleDbContext(DbContextOptions<PuzzleDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PuzzleDbContext).Assembly);
    }
}
