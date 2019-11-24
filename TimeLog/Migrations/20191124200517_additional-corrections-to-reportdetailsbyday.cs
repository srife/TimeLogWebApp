using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class additionalcorrectionstoreportdetailsbyday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
/****** Object:  StoredProcedure [dbo].[sp_ReportDetailsByDay]    Script Date: 11/24/2019 2:41:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ReportDetailsByDay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ReportDetailsByDay]
GO

/****** Object:  StoredProcedure [dbo].[sp_ReportDetailsByDay]    Script Date: 11/24/2019 2:41:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Tom Campbell
-- Create date: 2019-11-10
-- Description:	Create a summary of time frame's billing information
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReportDetailsByDay]
	@StartDate datetimeoffset(7),
	@EndDate datetimeoffset(7),
	@Rate money,
	@Billable bit = 1
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @daysTable TABLE ([date] DATETIMEOFFSET NOT NULL, [shortdate] DATE NOT NULL, [dayofweek] NVARCHAR(25) NOT NULL)
	INSERT INTO @daysTable ([date],[shortdate],[dayofweek])
		SELECT DATEADD(DD,ID-1,@StartDate) as [date]
			,CAST(DATEADD(DD,ID-1,@StartDate) AS DATE) AS [shortdate]
			,FORMAT(DATEADD(DD,ID-1,@StartDate), 'dddd') AS [DayOfWeek]
			--,day(dateadd(DD,ID-1,@StartDate)) as [DAY]
			--,month(dateadd(DD,ID-1,@StartDate)) as [MONTH]
			--,year(dateadd(DD,ID-1,@StartDate)) as [YEAR]
		FROM dbo.Tally
		WHERE DATEADD(DD,ID-1,@StartDate) <= @EndDate

	IF (@Billable = 0 OR @Billable IS NULL)
		BEGIN
			;WITH CTE AS(
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
				WHERE StartTime > @StartDate AND EndTime < @EndDate
			)
			SELECT ROW_NUMBER() OVER(ORDER BY dt.[shortdate]) AS Id
				, dt.[shortdate] AS StartDay
				, dt.[dayofweek] AS [DayOfWeek]
				, ISNULL(SUM(RoundedHours),0) AS Hrs
				, ISNULL(SUM(BillableAmt),0) AS Amt
			FROM CTE
			RIGHT JOIN @daysTable dt ON CTE.StartDay = dt.[shortdate]
			GROUP BY dt.[shortdate], CTE.StartDay, [DayOfWeek]
		END
	ELSE
		BEGIN
			;WITH CTE AS(
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
			)
			SELECT ROW_NUMBER() OVER(ORDER BY dt.[shortdate]) AS Id
				, dt.[shortdate] AS StartDay
				, dt.[dayofweek] AS [DayOfWeek]
				, ISNULL(SUM(RoundedHours),0) AS Hrs
				, ISNULL(SUM(BillableAmt),0) AS Amt
			FROM CTE
			RIGHT JOIN @daysTable dt ON CTE.StartDay = dt.[shortdate]
			GROUP BY dt.[shortdate], CTE.StartDay, [DayOfWeek]
		END
END
GO";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}