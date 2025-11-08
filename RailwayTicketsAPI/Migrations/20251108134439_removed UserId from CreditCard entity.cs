using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedUserIdfromCreditCardentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CreditCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CreditCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
