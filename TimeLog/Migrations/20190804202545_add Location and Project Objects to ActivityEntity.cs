using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class addLocationandProjectObjectstoActivityEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_LocationId",
                table: "ActivityEntity",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntity_ProjectId",
                table: "ActivityEntity",
                column: "ProjectId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Locations_LocationId",
                table: "ActivityEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityEntity_Projects_ProjectId",
                table: "ActivityEntity");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_LocationId",
                table: "ActivityEntity");

            migrationBuilder.DropIndex(
                name: "IX_ActivityEntity_ProjectId",
                table: "ActivityEntity");
        }
    }
}
