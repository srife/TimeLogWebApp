using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class StoredProcedureReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
-- =============================================
-- Author:		Tom Campbell
-- Create date: 2019-11-10
-- Description:	Create a summary of time frame's billing information
-- =============================================
CREATE PROCEDURE [dbo].[sp_Report]
	@StartDate datetimeoffset(7),
	@EndDate datetimeoffset(7),
	@Rate money,
	@Billable bit = NULL
AS
BEGIN
	SET NOCOUNT ON;

	;WITH CTE AS(
		SELECT Id
			,ClientId
			,ProjectId
			,Billable
			,DATEDIFF(second,StartTime, EndTime)  AS [TotalSeconds]
			,CEILING(DATEDIFF(second,StartTime, EndTime) * 1.0 / 900) * 900 AS [RoundedTotalSeconds]  --rounded to next 15 minutes
		FROM [dbo].[ActivityEntity]
		WHERE StartTime > @StartDate AND EndTime < @EndDate AND Billable = @Billable
	)

	SELECT ClientId, ProjectId, Billable
		,CAST(SUM([RoundedTotalSeconds]) / 3600 AS decimal(18,2)) AS [Duration]
		,CAST(CAST(SUM([RoundedTotalSeconds]) / 3600 AS decimal(18,2)) * @Rate AS smallmoney) AS BillableAmount
	FROM CTE
	GROUP BY ClientId, ProjectId, Billable
END
";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}