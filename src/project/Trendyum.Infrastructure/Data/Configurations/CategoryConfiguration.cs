using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trendyum.Domain.Entities;

namespace Trendyum.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", "Trendyum").HasKey(o => o.Id);
        builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
        builder.Property(p => p.Name).HasMaxLength(64).IsRequired();
    }
}
