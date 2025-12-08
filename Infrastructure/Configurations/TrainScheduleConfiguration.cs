using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TrainScheduleConfiguration : IEntityTypeConfiguration<TrainSchedule>
{
    public void Configure(EntityTypeBuilder<TrainSchedule> builder)
    {
        builder.HasKey(pk => pk.TrainScheduleId);

        builder.HasOne(ts => ts.Train)
            .WithMany(t => t.TrainSchedules)
            .HasForeignKey(fk => fk.TrainId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
