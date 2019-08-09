using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class update17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ActivityEntity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_ParentId",
                table: "ActivityEntity",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_ActivityEntity_ParentId",
                table: "ActivityEntity",
                column: "ParentId",
                principalTable: "ActivityEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_ActivityEntity_ParentId",
                table: "ActivityEntity");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_ParentId",
                table: "ActivityEntity");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ActivityEntity");
        }
    }
}
