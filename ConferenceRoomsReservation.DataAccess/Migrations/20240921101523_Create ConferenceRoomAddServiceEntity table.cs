using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRoomsReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateConferenceRoomAddServiceEntitytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddServices_ConferenceRooms_Id",
                table: "AddServices");

            migrationBuilder.CreateTable(
                name: "ConferenceRoomAddServices",
                columns: table => new
                {
                    ConferenceRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceRoomAddServices", x => new { x.ConferenceRoomId, x.AddServiceId });
                    table.ForeignKey(
                        name: "FK_ConferenceRoomAddServices_AddServices_AddServiceId",
                        column: x => x.AddServiceId,
                        principalTable: "AddServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConferenceRoomAddServices_ConferenceRooms_ConferenceRoomId",
                        column: x => x.ConferenceRoomId,
                        principalTable: "ConferenceRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceRoomAddServices_AddServiceId",
                table: "ConferenceRoomAddServices",
                column: "AddServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConferenceRoomAddServices");

            migrationBuilder.AddForeignKey(
                name: "FK_AddServices_ConferenceRooms_Id",
                table: "AddServices",
                column: "Id",
                principalTable: "ConferenceRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
