using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class switchtodatetimeoffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Month",
            //    table: "Summary");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "SumTotalDurationHours",
            //    table: "Summary",
            //    type: "decimal(8,1)",
            //    nullable: false,
            //    oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartTime",
                table: "ActivityEntity",
                type: "datetimeoffset(7)",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndTime",
                table: "ActivityEntity",
                type: "datetimeoffset(7)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<decimal>(
            //    name: "SumTotalDurationHours",
            //    table: "Summary",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(8,1)");

            //migrationBuilder.AddColumn<string>(
            //    name: "Month",
            //    table: "Summary",
            //    nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "ActivityEntity",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(7)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "ActivityEntity",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(7)",
                oldNullable: true);
        }
    }
}