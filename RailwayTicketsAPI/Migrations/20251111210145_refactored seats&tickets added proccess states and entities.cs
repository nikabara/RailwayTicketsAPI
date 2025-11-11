using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class refactoredseatsticketsaddedproccessstatesandentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOccupied",
                table: "Seats");

            migrationBuilder.AddColumn<int>(
                name: "TicketPaymentStatusId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatStatusId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PaymentStatuses",
                columns: table => new
                {
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatusName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatuses", x => x.PaymentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    SeatStatusId = table.Column<int>(type: "int", nullable: false),
                    SeatStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.SeatStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketPaymentStatusId",
                table: "Tickets",
                column: "TicketPaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatStatusId",
                table: "Seats",
                column: "SeatStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Statuses_SeatStatusId",
                table: "Seats",
                column: "SeatStatusId",
                principalTable: "Statuses",
                principalColumn: "SeatStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PaymentStatuses_TicketPaymentStatusId",
                table: "Tickets",
                column: "TicketPaymentStatusId",
                principalTable: "PaymentStatuses",
                principalColumn: "PaymentStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Statuses_SeatStatusId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PaymentStatuses_TicketPaymentStatusId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "PaymentStatuses");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketPaymentStatusId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SeatStatusId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "TicketPaymentStatusId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeatStatusId",
                table: "Seats");

            migrationBuilder.AddColumn<bool>(
                name: "IsOccupied",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
