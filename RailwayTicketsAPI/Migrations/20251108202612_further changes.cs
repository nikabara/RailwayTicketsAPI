using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class furtherchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CreditCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CreditCards");
        }
    }
}
