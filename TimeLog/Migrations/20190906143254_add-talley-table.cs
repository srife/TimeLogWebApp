using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class addtalleytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Summary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Month = table.Column<string>(nullable: true),
                    DayOfWeek = table.Column<string>(nullable: true),
                    SumTotalDurationHours = table.Column<decimal>(type: "decimal(8,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summary", x => x.Id);
                });

            var tallyTable = @"
set nocount on;

if object_id('dbo.Tally') is not null drop table dbo.tally

go

select top 10000 identity(int,1,1) as ID

into dbo.Tally from master.dbo.SysColumns

alter table dbo.Tally

add constraint PK_ID primary key clustered(ID)

go
";
            migrationBuilder.Sql(tallyTable);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Summary");

            migrationBuilder.DropTable(
                name: "Talley");
        }
    }
}