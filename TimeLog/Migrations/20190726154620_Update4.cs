using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Client",
                table: "ActivityEntity");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ActivityEntity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "ActivityEntity",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "ActivityEntity",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ActivityEntity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "ActivityEntity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_ClientId",
                table: "ActivityEntity",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_TypeId",
                table: "ActivityEntity",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Client_ClientId",
                table: "ActivityEntity",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_ActivityType_TypeId",
                table: "ActivityEntity",
                column: "TypeId",
                principalTable: "ActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Client_ClientId",
                table: "ActivityEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_ActivityType_TypeId",
                table: "ActivityEntity");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_ClientId",
                table: "ActivityEntity");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_TypeId",
                table: "ActivityEntity");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ActivityEntity");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "ActivityEntity");

            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "ActivityEntity",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "EndTime",
                table: "ActivityEntity",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "ActivityEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ActivityEntity",
                nullable: true);
        }
    }
}
