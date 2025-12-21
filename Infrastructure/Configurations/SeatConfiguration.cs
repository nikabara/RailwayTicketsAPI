using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(pk => pk.SeatId);

        builder.HasOne(s => s.Vagon)
            .WithMany(v => v.Seats)
            .HasForeignKey(s => s.VagonId)
            // CHANGE THIS LINE:
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.SeatStatus)
            .WithMany(ss => ss.Seats)
            .HasForeignKey(fk => fk.SeatStatusId);
    }
}