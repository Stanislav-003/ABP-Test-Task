using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomsReservation.DataAccess.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
{
    public void Configure(EntityTypeBuilder<BookingEntity> builder)
    {
        builder.ToTable("Bookings");
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
           .ValueGeneratedOnAdd();

        builder.Property(b => b.TotalPrice)
            .HasColumnType("decimal(18,2)");

        builder.ComplexProperty(b => b.BookingTime, c =>
        {
            c.IsRequired();
            c.Property(a => a.Year).HasColumnName("Year");
            c.Property(a => a.Month).HasColumnName("Month");
            c.Property(a => a.Day).HasColumnName("Day");
            c.Property(a => a.Hours).HasColumnName("Hours");
            c.Property(a => a.Minutes).HasColumnName("Minutes");
        });

        builder.Property(b => b.Duration)
            .IsRequired();

        builder.HasOne(b => b.ConferenceRoom)
            .WithMany()
            .HasForeignKey(b => b.ConferenceRoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
