using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Seat> Seats { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Train> Trains { get; set; }
    public DbSet<Vagon> Vagons { get; set; }
    public DbSet<TrainSchedule> TrainSchedule { get; set; }
    public DbSet<User> Users { get; set; }
}
