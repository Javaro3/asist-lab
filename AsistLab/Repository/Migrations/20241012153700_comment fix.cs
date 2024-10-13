using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class commentfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TripId",
                table: "Comments",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Trips_TripId",
                table: "Comments",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Trips_TripId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TripId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Comments");
        }
    }
}
