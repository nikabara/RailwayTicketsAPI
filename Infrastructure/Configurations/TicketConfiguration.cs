using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(pk => pk.TicketId);

        builder.HasOne(t => t.Seat)
            .WithMany(s => s.Tickets)
            .HasForeignKey(fk => fk.SeatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(fk => fk.UserId);

        builder.HasOne(t => t.PaymentStatus)
            .WithMany(ps => ps.Tickets)
            .HasForeignKey(fk => fk.TicketPaymentStatusId);
    }
}
