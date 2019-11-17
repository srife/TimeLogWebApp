using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class spreportdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Report",
            //    columns: table => new
            //    {
            //        ClientId = table.Column<int>(nullable: false),
            //        ProjectId = table.Column<int>(nullable: false),
            //        Billable = table.Column<bool>(nullable: false),
            //        Duration = table.Column<decimal>(nullable: false),
            //        BillableAmount = table.Column<decimal>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Report", x => new { x.ClientId, x.ProjectId });
            //        table.ForeignKey(
            //            name: "FK_Report_Clients_ClientId",
            //            column: x => x.ClientId,
            //            principalTable: "Clients",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Report_Projects_ProjectId",
            //            column: x => x.ProjectId,
            //            principalTable: "Projects",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Report_ProjectId",
            //    table: "Report",
            //    column: "ProjectId");
            var sp = @"
-- =============================================
-- Author:		Tom Campbell
-- Create date: 2019-11-10
-- Description:	Create a summary of time frame's billing information
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReportDetails]
	@StartDate datetimeoffset(7),
	@EndDate datetimeoffset(7),
	@Rate money,
	@Billable bit = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id]
		,CAST(StartTime as date) AS StartDay
		,CAST([StartTime] AS time(0)) AS StartTime
		,CAST([EndTime] as time(0)) AS EndTime
		,CAST(CONVERT(TIME,CAST(DATEDIFF(second,StartTime, EndTime) / 3600 AS varchar(2)) + ':' + CAST((DATEDIFF(second,StartTime, EndTime) % 3600) / 60 as varchar(2))) AS time(0))  AS [Duration]
		,(CEILING(DATEDIFF(second, StartTime,EndTime) * 1.0 / 900) * 900) / 3600 AS [RoundedHours]
		,CAST(((CEILING(DATEDIFF(second, StartTime,EndTime) * 1.0 / 900) * 900) / 3600) * @Rate as money) AS BillableAmt
		,(SELECT c.[Name] FROM Clients c WHERE c.Id = [ClientId]) AS ClientName
		,(SELECT p.Name FROM Projects p WHERE p.Id = ProjectId) AS ProjectName
		,(SELECT a.Name FROM ActivityTypes a where a.Id = ActivityTypeId) as ActivityType
		,[Billable]
		,[Tasks]
		,[InvoiceStatement]
  FROM [dbo].[ActivityEntity]
  WHERE StartTime > @StartDate AND EndTime < @EndDate AND Billable = @Billable
  ORDER BY ActivityEntity.StartTime
END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");
        }
    }
}