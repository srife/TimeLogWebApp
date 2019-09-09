using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class spsummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
/****** Object:  StoredProcedure [dbo].[sp_Summary]    Script Date: 9/9/2019 10:09:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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
		SELECT ae.[Id]
			  ,d.date AS [Date]
			  ,DATEPART(ww,d.[date]) AS WeekNumber
			  ,DATENAME(WEEKDAY,d.[date]) AS [DayOfWeek]
			  ,CAST(
				ISNULL((DATEDIFF(second,ae.StartTime,ISNULL(ae.EndTime,GETDATE())) / 3600.0),0) AS decimal(8,1)
				) AS TotalDurationHours
			  ,ClientId
		  FROM [dbo].[ActivityEntity] ae
		  RIGHT OUTER JOIN @dates d ON CAST(ae.StartTime AS Date) = d.[date]
	)

	SELECT CAST(ROW_NUMBER() OVER (ORDER BY d.[Date]) AS int) AS Id
	    ,d.[Date]
		,DATENAME(WEEKDAY,d.[Date]) AS [DayOfWeek]
		,(SELECT ISNULL(SUM(cte2.TotalDurationHours),0) FROM CTE cte2 WHERE cte2.[Date] = d.[date] AND (cte2.ClientId = 2 OR cte2.ClientId IS NULL)) AS SumTotalDurationHours
	FROM CTE
	RIGHT OUTER JOIN @dates d ON CTE.[Date] = d.[date]
	GROUP By d.[Date]
	ORDER BY d.[Date]
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