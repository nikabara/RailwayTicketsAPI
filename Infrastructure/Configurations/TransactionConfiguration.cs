using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(pk => pk.TransactionId);

        builder.HasOne(t => t.TransactionState)
            .WithMany(t => t.Transactions)
            .HasForeignKey(fk => fk.TransactionStateId);

        builder.HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(fk => fk.UserId);

        builder.HasOne(t => t.Seat)
            .WithMany(s => s.Transactions)
            .HasForeignKey(fk => fk.SeatId);

        builder.HasOne(t => t.TrainSchedule)
            .WithMany(ts => ts.Transactions)
            .HasForeignKey(fk => fk.TrainScheduleId);

        builder.HasOne(t => t.Currency)
            .WithMany(s => s.Transactions)
            .HasForeignKey(fk => fk.CurrencyId);
    }
}
