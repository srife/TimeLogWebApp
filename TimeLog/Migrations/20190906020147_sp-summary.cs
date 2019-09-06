using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class spsummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
CREATE PROCEDURE [dbo].[sp_Summary]
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @currentWeekOfYear int = (SELECT DATEPART(week,GETDATE()))

	;WITH CTE AS(
		SELECT [Id]
			  ,CONVERT(date,StartTime) AS Date
			  ,[StartTime]
			  ,[EndTime]
			  ,DATEPART(DAYOFYEAR,StartTime) AS [DayOfYear]
			  ,DATEPART(week,StartTime) AS [WeekOfYear]
			  ,DATENAME(WEEKDAY,StartTime) AS [DayOfWeek]
			  ,DATEDIFF(second,StartTime,EndTime) AS TotalSeconds
			  ,(DATEDIFF(second,StartTime,EndTime) / 60)  AS TotalMinutes
			  ,DATEDIFF(minute,StartTime,EndTime) / 60 AS TotalHours
			  ,DATEDIFF(minute,StartTime,EndTime) % 60 AS TotalRemainerMinutes
			  ,CAST((DATEDIFF(second,StartTime,EndTime) / 3600.0) AS decimal(8,1)) AS TotalDurationHours
			  ,ProjectId
		  FROM [dbo].[ActivityEntity]
		  WHERE EndTime IS NOT NULL AND DATEPART(week,StartTime) = @currentWeekOfYear
	)
	SELECT [Date]
		,DATENAME(MONTH,[date]) AS [Month]
		,DATENAME(WEEKDAY,[date]) AS [DayOfWeek]
		,SUM(TotalDurationHours) AS SumTotalDurationHours
		,ProjectId
		,(SELECT ISNULL(p.Name, '') FROM Projects p where p.Id = CTE.ProjectId) AS ProjectName
	FROM CTE
	GROUP By [Date], ProjectId
	ORDER BY [Date]
END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}