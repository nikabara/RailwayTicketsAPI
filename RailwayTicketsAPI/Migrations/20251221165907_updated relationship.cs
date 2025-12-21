using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Vagons_VagonId",
                table: "Seats");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Vagons_VagonId",
                table: "Seats",
                column: "VagonId",
                principalTable: "Vagons",
                principalColumn: "VagonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Vagons_VagonId",
                table: "Seats");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Vagons_VagonId",
                table: "Seats",
                column: "VagonId",
                principalTable: "Vagons",
                principalColumn: "VagonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
