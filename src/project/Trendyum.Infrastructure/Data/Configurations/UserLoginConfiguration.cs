using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Trendyum.Domain.Entities;

namespace Trendyum.Infrastructure.Data.Configurations;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable("UserLogins", "Identity");
    }
}