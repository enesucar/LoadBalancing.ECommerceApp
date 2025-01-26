using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Trendyum.Domain.Entities;

namespace Trendyum.Infrastructure.Data.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("Resources", "Trendyum").HasKey(o => o.Id);
        builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
        builder.Property(p => p.Name).HasMaxLength(256).IsRequired();
        builder.Property(p => p.Path).IsRequired();
        builder.Property(p => p.Type).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Size).IsRequired();
        builder.Property(p => p.CreationDate).HasColumnType("datetime").IsRequired();
    }
}