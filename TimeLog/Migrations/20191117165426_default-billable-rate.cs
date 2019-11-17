using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class defaultbillablerate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<decimal>(
            //    name: "BillableAmount",
            //    table: "Report",
            //    type: "money",
            //    nullable: false,
            //    oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultBillableRate",
                table: "Projects",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultBillableRate",
                table: "Clients",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            //migrationBuilder.CreateTable(
            //    name: "ReportDetailsByDay",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        StartDay = table.Column<DateTime>(nullable: false),
            //        DayOfWeek = table.Column<string>(nullable: true),
            //        Hrs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Amt = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReportDetailsByDay", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ReportDetailsByDay");

            migrationBuilder.DropColumn(
                name: "DefaultBillableRate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DefaultBillableRate",
                table: "Clients");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "BillableAmount",
            //    table: "Report",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "money");
        }
    }
}