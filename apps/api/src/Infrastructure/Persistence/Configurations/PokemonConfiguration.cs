namespace Infrastructure.Persistence.Configurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.PokedexNumber).IsUnique();

        builder.Property(p => p.Name).HasMaxLength(100);
        builder.Property(p => p.SpriteUrl).HasMaxLength(500);
        builder.Property(p => p.Description).HasMaxLength(2000);

        builder.Property(p => p.PrimaryType).HasConversion<string>().HasMaxLength(50);
        builder.Property(p => p.SecondaryType).HasConversion<string>().HasMaxLength(50);
    }
}
