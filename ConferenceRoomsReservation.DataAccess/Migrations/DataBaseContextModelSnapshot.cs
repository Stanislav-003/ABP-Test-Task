﻿// <auto-generated />
using System;
using System.Collections.Generic;
using ConferenceRoomsReservation.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConferenceRoomsReservation.DataAccess.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConferenceRoomsReservation.DataAccess.Entities.AddServiceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("AddServices", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("a9ff0ac4-cfa5-4cd2-a4e6-da9ebad6dec6"),
                            Name = "Проєктор",
                            Price = 500m
                        },
                        new
                        {
                            Id = new Guid("5286ba76-551c-43c5-80e3-65df77156e12"),
                            Name = "Wi-Fi",
                            Price = 300m
                        },
                        new
                        {
                            Id = new Guid("ba4d1fa8-1cbe-433c-b253-85994b043b29"),
                            Name = "Звук",
                            Price = 700m
                        });
                });

            modelBuilder.Entity("ConferenceRoomsReservation.DataAccess.Entities.BookingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConferenceRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.ComplexProperty<Dictionary<string, object>>("BookingTime", "ConferenceRoomsReservation.DataAccess.Entities.BookingEntity.BookingTime#BookingTime", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Day")
                                .HasColumnType("int")
                                .HasColumnName("Day");

                            b1.Property<int>("Hours")
                                .HasColumnType("int")
                                .HasColumnName("Hours");

                            b1.Property<int>("Minutes")
                                .HasColumnType("int")
                                .HasColumnName("Minutes");

                            b1.Property<int>("Month")
                                .HasColumnType("int")
                                .HasColumnName("Month");

                            b1.Property<int>("Year")
                                .HasColumnType("int")
                                .HasColumnName("Year");
                        });

                    b.HasKey("Id");

                    b.HasIndex("ConferenceRoomId");

                    b.ToTable("Bookings", (string)null);
                });

            modelBuilder.Entity("ConferenceRoomsReservation.DataAccess.Entities.ConferenceRoomAddServiceEntity", b =>
                {
                    b.Property<Guid>("ConferenceRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ConferenceRoomId", "AddServiceId");

                    b.HasIndex("AddServiceId");

                    b.ToTable("ConferenceRoomAddServices", (string)null);
                });

            modelBuilder.Entity("ConferenceRoomsReservation.DataAccess.Entities.ConferenceRoomEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BasePricePerHour")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("ConferenceRooms", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("fc952cb5-a2cd-4e5d-9831-d5db44b56a4d"),
                            BasePricePerHour = 2000m,
                            Capacity = 50,
                            Name = "Зал А"
                        },
                        new
                        {
                            Id = new Guid("b68506c7-babb-457b-a1b1-971df150d0c7"),
                            BasePricePerHour = 3500m,
                            Capacity = 100,
                            Name = "Зал B"
                        },
                        new
                        {
                            Id = new Guid("13dff5d6-9913-4e38-b556-3fb6c676a57e"),
                            BasePricePerHour = 1500m,
                            Capacity = 30,
                            Name = "Зал C"
                        });
                });

            modelBuilder.Entity("ConferenceRoomsReservation.DataAccess.Entities.BookingEntity", b =>
                {
                    b.HasOne("ConferenceRoomsReservation.DataAccess.Entities.ConferenceRoomEntity", "ConferenceRoom")
                        .WithMany()
                        .HasForeignKey("ConferenceRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConferenceRoom");
                });

            modelBuilder.Entity("ConferenceRoomsReservation.DataAccess.Entities.ConferenceRoomAddServiceEntity", b =>
                {
                    b.HasOne("ConferenceRoomsReservation.DataAccess.Entities.AddServiceEntity", "AddService")
                        .WithMany("ConferenceRoomAddServices")
                        .HasForeignKey("AddServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConferenceRoomsReservation.DataAccess.Entities.ConferenceRoomEntity", "ConferenceRoom")
                        .WithMany("ConferenceRoomAddServices")
                        .HasForeignKey("ConferenceRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddService");

                    b.Navigation("ConferenceRoom");
                });

            modelBuilder.Entity("ConferenceRoomsReservation.DataAccess.Entities.AddServiceEntity", b =>
                {
                    b.Navigation("ConferenceRoomAddServices");
                });

            modelBuilder.Entity("ConferenceRoomsReservation.DataAccess.Entities.ConferenceRoomEntity", b =>
                {
                    b.Navigation("ConferenceRoomAddServices");
                });
#pragma warning restore 612, 618
        }
    }
}
