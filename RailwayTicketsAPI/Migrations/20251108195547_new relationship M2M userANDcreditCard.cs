using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class newrelationshipM2MuserANDcreditCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCreditCards");

            migrationBuilder.CreateTable(
                name: "CreditCardUser",
                columns: table => new
                {
                    CreditCardsCreditCardId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardUser", x => new { x.CreditCardsCreditCardId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_CreditCardUser_CreditCards_CreditCardsCreditCardId",
                        column: x => x.CreditCardsCreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "CreditCardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCardUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardUser_UsersUserId",
                table: "CreditCardUser",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCardUser");

            migrationBuilder.CreateTable(
                name: "UserCreditCards",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreditCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCreditCards", x => new { x.UserId, x.CreditCardId });
                    table.ForeignKey(
                        name: "FK_UserCreditCards_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "CreditCardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCreditCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCreditCards_CreditCardId",
                table: "UserCreditCards",
                column: "CreditCardId");
        }
    }
}
