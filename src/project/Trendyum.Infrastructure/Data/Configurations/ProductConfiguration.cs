using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Trendyum.Domain.Entities;

namespace Trendyum.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "Trendyum").HasKey(o => o.Id);
        builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
        builder.Property(p => p.Name).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.CategoryId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18, 4)").IsRequired();
        builder.Property(p => p.Quantity).HasColumnType("int").IsRequired();
        builder.Property(p => p.ImageId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.CreationDate).HasColumnType("datetime").IsRequired();
    }
}