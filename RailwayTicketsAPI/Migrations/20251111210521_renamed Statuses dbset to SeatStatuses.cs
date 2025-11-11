using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class renamedStatusesdbsettoSeatStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Statuses_SeatStatusId",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "SeatStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatStatuses",
                table: "SeatStatuses",
                column: "SeatStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatStatuses_SeatStatusId",
                table: "Seats",
                column: "SeatStatusId",
                principalTable: "SeatStatuses",
                principalColumn: "SeatStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatStatuses_SeatStatusId",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatStatuses",
                table: "SeatStatuses");

            migrationBuilder.RenameTable(
                name: "SeatStatuses",
                newName: "Statuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "SeatStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Statuses_SeatStatusId",
                table: "Seats",
                column: "SeatStatusId",
                principalTable: "Statuses",
                principalColumn: "SeatStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
