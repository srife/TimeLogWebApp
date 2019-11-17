using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class addbillingrateandactualbillablehours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualBilledDuration",
                table: "ActivityEntity",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BillableRate",
                table: "ActivityEntity",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualBilledDuration",
                table: "ActivityEntity");

            migrationBuilder.DropColumn(
                name: "BillableRate",
                table: "ActivityEntity");
        }
    }
}
