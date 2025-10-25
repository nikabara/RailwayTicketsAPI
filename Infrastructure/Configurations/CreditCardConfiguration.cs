using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
{
    public void Configure(EntityTypeBuilder<CreditCard> builder)
    {
        builder.HasKey(pk => pk.CreditCardId);

        builder.HasOne(cc => cc.CreditCardIssuer)
            .WithMany(cci => cci.CreditCards)
            .HasForeignKey(fk => fk.CreditCardIssuerId);
    }
}
