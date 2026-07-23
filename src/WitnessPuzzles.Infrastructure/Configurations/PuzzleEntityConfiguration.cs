using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WitnessPuzzles.Infrastructure.Entities;

namespace WitnessPuzzles.Infrastructure.Configurations;

public class PuzzleEntityConfiguration : IEntityTypeConfiguration<PuzzleEntity>
{
    public void Configure(EntityTypeBuilder<PuzzleEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.OwnsMany(e => e.NodeModifiers, b =>
        {
            b.WithOwner().HasForeignKey("PuzzleId");
            b.Property<int>("Id").ValueGeneratedOnAdd();
            b.HasKey("Id");
        });
        builder.OwnsMany(e => e.EdgeModifiers, b =>
        {
            b.WithOwner().HasForeignKey("PuzzleId");
            b.Property<int>("Id").ValueGeneratedOnAdd();
            b.HasKey("Id");
        });
        builder.OwnsMany(e => e.CellModifiers, b =>
        {
            b.WithOwner().HasForeignKey("PuzzleId");
            b.Property<int>("Id").ValueGeneratedOnAdd();
            b.HasKey("Id");
        });
    }
}
