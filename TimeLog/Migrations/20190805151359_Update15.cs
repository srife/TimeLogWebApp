using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class Update15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Clients_ClientId",
                table: "ActivityEntity");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ActivityEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Clients_ClientId",
                table: "ActivityEntity",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Clients_ClientId",
                table: "ActivityEntity");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ActivityEntity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Clients_ClientId",
                table: "ActivityEntity",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
