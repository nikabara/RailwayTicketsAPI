using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeduserrolemanagementaddedUserRoletableandUserRoleTypeenum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add the new UserRoleId column to Users table with a temporary default of 0.
            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0); // Defaulting to 0 for existing users

            // 2. Create the UserRoles lookup table.
            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                });

            // --- FIX 1: Seed the UserRoles table with valid IDs (1, 2, 3, etc.) ---
            // These IDs must match your UserRoleType enum values (if 1=Customer, 2=Admin)
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "UserRoleName" },
                values: new object[,]
                {
                    { 1, "SuperAdmin" }, // Assuming 1 corresponds to your default Customer/Basic Role
                    { 2, "Admin" },    // Assuming 2 corresponds to your Admin Role
                    { 3, "User" }      // Assuming 3 corresponds to your User Role
                    // Add other roles as necessary
                });
            // ----------------------------------------------------------------------

            // --- FIX 2: Update all existing Users from the invalid ID (0) to a valid ID (1) ---
            migrationBuilder.Sql(
                "UPDATE [Users] SET [UserRoleId] = 3 WHERE [UserRoleId] = 0"
            );
            // ----------------------------------------------------------------------

            // 3. Create the index.
            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            // 4. Add the Foreign Key constraint (which will now succeed).
            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserRoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Users");
        }
    }
}
