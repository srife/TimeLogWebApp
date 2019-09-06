using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class spsummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Summary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_Summary] AS'
END
GO

ALTER PROCEDURE [dbo].[sp_Summary]
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StartDate datetime = (SELECT DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0))
	DECLARE @EndDate datetime = @StartDate+6

	DECLARE @dates TABLE ([date] datetime NOT NULL)
	INSERT INTO @dates ([date])
	SELECT DATEADD(DD,ID-1,@StartDate) as [date]
	FROM dbo.Tally
	WHERE DATEADD(DD,ID-1,@StartDate)<=@EndDate

	;WITH CTE AS(
		SELECT [Id]
			  ,d.date AS [Date]
			  ,DATENAME(WEEKDAY,StartTime) AS [DayOfWeek]
			  ,CAST((DATEDIFF(second,StartTime,ISNULL(EndTime,GETDATE())) / 3600.0) AS decimal(8,1)) AS TotalDurationHours
		  FROM [dbo].[ActivityEntity]
		  RIGHT OUTER JOIN @dates d ON CAST(ActivityEntity.StartTime AS Date) = d.[date]
	)

	SELECT CAST(ROW_NUMBER() OVER (ORDER BY [Date]) AS int) AS Id
	    ,[Date]
		,DATENAME(WEEKDAY,[Date]) AS [DayOfWeek]
		,ISNULL(SUM(TotalDurationHours),0) AS SumTotalDurationHours
	FROM CTE
	GROUP By [Date]
	ORDER BY [Date]
END
GO
";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"
DROP PROCEDURE IF EXISTS [dbo].[sp_Summary]
GO";

            migrationBuilder.Sql(sp);
        }
    }
}