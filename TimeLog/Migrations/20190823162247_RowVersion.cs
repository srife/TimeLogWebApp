using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class RowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ActivityEntity",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DefaultActivityTypeId",
                table: "Projects",
                column: "DefaultActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DefaultClientId",
                table: "Projects",
                column: "DefaultClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DefaultLocationId",
                table: "Projects",
                column: "DefaultLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ActivityTypes_DefaultActivityTypeId",
                table: "Projects",
                column: "DefaultActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Clients_DefaultClientId",
                table: "Projects",
                column: "DefaultClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Locations_DefaultLocationId",
                table: "Projects",
                column: "DefaultLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ActivityTypes_DefaultActivityTypeId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Clients_DefaultClientId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Locations_DefaultLocationId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DefaultActivityTypeId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DefaultClientId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DefaultLocationId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ActivityEntity");
        }
    }
}
