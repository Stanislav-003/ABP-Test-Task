using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomsReservation.DataAccess.Configurations;

public class ConferenceRoomAddServiceConfiguration : IEntityTypeConfiguration<ConferenceRoomAddServiceEntity>
{
    public void Configure(EntityTypeBuilder<ConferenceRoomAddServiceEntity> builder)
    {
        builder.ToTable("ConferenceRoomAddServices");

        builder.HasKey(cras => new { cras.ConferenceRoomId, cras.AddServiceId });

        builder.HasOne(cras => cras.ConferenceRoom)
            .WithMany(c => c.ConferenceRoomAddServices)
            .HasForeignKey(cras => cras.ConferenceRoomId);

        builder.HasOne(cras => cras.AddService)
            .WithMany(a => a.ConferenceRoomAddServices)
            .HasForeignKey(cras => cras.AddServiceId);
    }
}
