using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class Update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Clients_ClientId",
                table: "ActivityEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_ActivityTypes_TypeId",
                table: "ActivityEntity");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_ClientId",
                table: "ActivityEntity");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_TypeId",
                table: "ActivityEntity");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "ActivityEntity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ActivityEntity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "ActivityEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ActivityEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_ClientId",
                table: "ActivityEntity",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_TypeId",
                table: "ActivityEntity",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Clients_ClientId",
                table: "ActivityEntity",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_ActivityTypes_TypeId",
                table: "ActivityEntity",
                column: "TypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
