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
            .HasForeignKey(fk => fk.SeatId);
    }
}
