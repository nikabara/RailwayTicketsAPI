using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(pk => pk.SeatId);

        builder.HasOne(s => s.Vagon)
            .WithMany(v => v.Seats)
            .HasForeignKey(s => s.VagonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.SeatStatus)
            .WithMany(ss => ss.Seats)
            .HasForeignKey(fk => fk.SeatStatusId);
    }
}
