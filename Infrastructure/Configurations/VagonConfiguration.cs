using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VagonConfiguration : IEntityTypeConfiguration<Vagon>
{
    public void Configure(EntityTypeBuilder<Vagon> builder)
    {
        builder.HasKey(pk => pk.VagonId);

        builder.HasOne(v => v.Train)
            .WithMany(t => t.Vagons)
            .HasForeignKey(fk => fk.TrainId);
    }
}
