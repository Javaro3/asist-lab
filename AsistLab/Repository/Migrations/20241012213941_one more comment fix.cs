using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class onemorecommentfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Comments",
                newName: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Comments",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
