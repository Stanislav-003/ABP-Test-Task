using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConferenceRoomsReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AddServices",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("5286ba76-551c-43c5-80e3-65df77156e12"), "Wi-Fi", 300m },
                    { new Guid("a9ff0ac4-cfa5-4cd2-a4e6-da9ebad6dec6"), "Проєктор", 500m },
                    { new Guid("ba4d1fa8-1cbe-433c-b253-85994b043b29"), "Звук", 700m }
                });

            migrationBuilder.InsertData(
                table: "ConferenceRooms",
                columns: new[] { "Id", "BasePricePerHour", "Capacity", "Name" },
                values: new object[,]
                {
                    { new Guid("13dff5d6-9913-4e38-b556-3fb6c676a57e"), 1500m, 30, "Зал C" },
                    { new Guid("b68506c7-babb-457b-a1b1-971df150d0c7"), 3500m, 100, "Зал B" },
                    { new Guid("fc952cb5-a2cd-4e5d-9831-d5db44b56a4d"), 2000m, 50, "Зал А" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AddServices",
                keyColumn: "Id",
                keyValue: new Guid("5286ba76-551c-43c5-80e3-65df77156e12"));

            migrationBuilder.DeleteData(
                table: "AddServices",
                keyColumn: "Id",
                keyValue: new Guid("a9ff0ac4-cfa5-4cd2-a4e6-da9ebad6dec6"));

            migrationBuilder.DeleteData(
                table: "AddServices",
                keyColumn: "Id",
                keyValue: new Guid("ba4d1fa8-1cbe-433c-b253-85994b043b29"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("13dff5d6-9913-4e38-b556-3fb6c676a57e"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("b68506c7-babb-457b-a1b1-971df150d0c7"));

            migrationBuilder.DeleteData(
                table: "ConferenceRooms",
                keyColumn: "Id",
                keyValue: new Guid("fc952cb5-a2cd-4e5d-9831-d5db44b56a4d"));
        }
    }
}
