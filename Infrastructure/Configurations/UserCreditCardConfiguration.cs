using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserCreditCardConfiguration : IEntityTypeConfiguration<UserCreditCard>
{
    public void Configure(EntityTypeBuilder<UserCreditCard> builder)
    {
        builder.HasKey(pks => new { pks.UserId, pks.CreditCardId });

        builder.HasOne(uc => uc.User)
               .WithMany(ucc => ucc.UserCreditCards)
               .HasForeignKey(fk => fk.UserId);

        builder.HasOne(uc => uc.CreditCard)
               .WithMany(ucc => ucc.UserCreditCards)
               .HasForeignKey(fk => fk.CreditCardId);
    }
}
