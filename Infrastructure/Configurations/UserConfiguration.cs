using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(pk => pk.UserId);

        builder.HasOne(u => u.UserRole)
            .WithMany(ur => ur.Users)
            .HasForeignKey(fk => fk.UserRoleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(u => u.UserRole)
            .WithMany(ur => ur.Users)
            .HasForeignKey(fk => fk.UserRoleId);
    }
}
