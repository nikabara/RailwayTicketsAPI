using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayTicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeddeletionbehaviourtocascadetraintrainschedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainSchedule_Trains_TrainId",
                table: "TrainSchedule");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainSchedule_Trains_TrainId",
                table: "TrainSchedule",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "TrainId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainSchedule_Trains_TrainId",
                table: "TrainSchedule");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainSchedule_Trains_TrainId",
                table: "TrainSchedule",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "TrainId");
        }
    }
}
