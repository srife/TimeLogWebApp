using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class Update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "ActivityEntity",
                newName: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_ActivityTypeId",
                table: "ActivityEntity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_ClientId",
                table: "ActivityEntity",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_ActivityTypes_ActivityTypeId",
                table: "ActivityEntity",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Clients_ClientId",
                table: "ActivityEntity",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_ActivityTypes_ActivityTypeId",
                table: "ActivityEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Clients_ClientId",
                table: "ActivityEntity");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_ActivityTypeId",
                table: "ActivityEntity");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_ClientId",
                table: "ActivityEntity");

            migrationBuilder.RenameColumn(
                name: "ActivityTypeId",
                table: "ActivityEntity",
                newName: "TypeId");
        }
    }
}
