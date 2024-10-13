using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class tripfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Trips",
                newName: "ExpectedStartTime");

            migrationBuilder.RenameColumn(
                name: "FinishTime",
                table: "Trips",
                newName: "ExpectedFinishTime");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Trips",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Trips",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RealFinishTime",
                table: "Trips",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RealStartTime",
                table: "Trips",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "RealFinishTime",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "RealStartTime",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "ExpectedStartTime",
                table: "Trips",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "ExpectedFinishTime",
                table: "Trips",
                newName: "FinishTime");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Trips",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
