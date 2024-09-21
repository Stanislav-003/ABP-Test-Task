using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomsReservation.DataAccess.Configurations;

public class AddServiceConfiguration : IEntityTypeConfiguration<AddServiceEntity>
{
    public void Configure(EntityTypeBuilder<AddServiceEntity> builder)
    {
        builder.ToTable("AddServices");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
           .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(AddService.MAX_NAME_LENGTH);

        builder.Property(a => a.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasMany(a => a.ConferenceRoomAddServices)
            .WithOne(cras => cras.AddService)
            .HasForeignKey(cras => cras.AddServiceId);
    }
}
