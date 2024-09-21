using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomsReservation.DataAccess.Configurations;

public class ConferenceRoomConfiguraton : IEntityTypeConfiguration<ConferenceRoomEntity>
{
    public void Configure(EntityTypeBuilder<ConferenceRoomEntity> builder)
    {
        builder.ToTable("ConferenceRooms");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(ConferenceRoom.MAX_NAME_LENGTH);

        builder.Property(c => c.Capacity)
            .IsRequired();

        builder.Property(c => c.BasePricePerHour)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasMany(c => c.ConferenceRoomAddServices)
            .WithOne(cras => cras.ConferenceRoom)
            .HasForeignKey(cras => cras.ConferenceRoomId);
    }
}
