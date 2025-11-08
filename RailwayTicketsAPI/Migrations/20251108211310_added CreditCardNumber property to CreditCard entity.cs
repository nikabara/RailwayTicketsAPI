using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedCreditCardNumberpropertytoCreditCardentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditCardNumber",
                table: "CreditCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCardNumber",
                table: "CreditCards");
        }
    }
}
