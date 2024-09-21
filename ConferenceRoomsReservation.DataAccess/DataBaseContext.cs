using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.DataAccess.Configurations;
using ConferenceRoomsReservation.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsReservation.DataAccess;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    public DbSet<ConferenceRoomEntity> ConferenceRooms { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }
    public DbSet<AddServiceEntity> Services { get; set; }
    public DbSet<ConferenceRoomAddServiceEntity> ConferenceRoomAddServices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
        modelBuilder.ApplyConfiguration(new AddServiceConfiguration());
        modelBuilder.ApplyConfiguration(new ConferenceRoomAddServiceConfiguration());
        modelBuilder.ApplyConfiguration(new ConferenceRoomConfiguraton());


        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ConferenceRoomEntity>().HasData(
            new ConferenceRoomEntity { Id = Guid.NewGuid(), Name = "Зал А", Capacity = 50, BasePricePerHour = 2000 },
            new ConferenceRoomEntity { Id = Guid.NewGuid(), Name = "Зал B", Capacity = 100, BasePricePerHour = 3500 },
            new ConferenceRoomEntity { Id = Guid.NewGuid(), Name = "Зал C", Capacity = 30, BasePricePerHour = 1500 }
        );

        modelBuilder.Entity<AddServiceEntity>().HasData(
            new AddServiceEntity { Id = Guid.NewGuid(), Name = "Проєктор", Price = 500 },
            new AddServiceEntity { Id = Guid.NewGuid(), Name = "Wi-Fi", Price = 300 },
            new AddServiceEntity { Id = Guid.NewGuid(), Name = "Звук", Price = 700 }
        );
    }
}
