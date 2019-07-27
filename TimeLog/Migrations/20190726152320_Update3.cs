using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ETag",
                table: "ActivityEntity");

            migrationBuilder.DropColumn(
                name: "PartitionKey",
                table: "ActivityEntity");

            migrationBuilder.DropColumn(
                name: "RowKey",
                table: "ActivityEntity");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "ActivityEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "ActivityEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartitionKey",
                table: "ActivityEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowKey",
                table: "ActivityEntity",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Timestamp",
                table: "ActivityEntity",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
