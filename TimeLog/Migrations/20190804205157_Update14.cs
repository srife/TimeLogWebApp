using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class Update14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Locations_LocationId",
                table: "ActivityEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Projects_ProjectId",
                table: "ActivityEntity");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ActivityEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "ActivityEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Locations_LocationId",
                table: "ActivityEntity",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Projects_ProjectId",
                table: "ActivityEntity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Locations_LocationId",
                table: "ActivityEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Projects_ProjectId",
                table: "ActivityEntity");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ActivityEntity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "ActivityEntity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Locations_LocationId",
                table: "ActivityEntity",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityEntity_Projects_ProjectId",
                table: "ActivityEntity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
