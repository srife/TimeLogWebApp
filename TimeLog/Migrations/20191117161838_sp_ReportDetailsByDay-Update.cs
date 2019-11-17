using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class sp_ReportDetailsByDayUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
/****** Object:  StoredProcedure [dbo].[sp_ReportDetailsByDay]    Script Date: 11/17/2019 11:15:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ReportDetailsByDay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ReportDetailsByDay]
GO

/****** Object:  StoredProcedure [dbo].[sp_ReportDetailsByDay]    Script Date: 11/17/2019 11:15:26 AM ******/
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
	@Billable bit = NULL
AS
BEGIN
	SET NOCOUNT ON;

	WITH CTE AS(
		SELECT [Id]
			,CAST(StartTime as date) AS StartDay
			,FORMAT(StartTime, 'dddd') AS [DayOfWeek]
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
	SELECT ROW_NUMBER() OVER(ORDER BY StartDay) AS Id, StartDay, [DayOfWeek], SUM(RoundedHours) AS Hrs, SUM(RoundedHours * @Rate) AS Amt
	FROM CTE
	GROUP BY StartDay, [DayOfWeek]
END
GO";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"
/****** Object:  StoredProcedure [dbo].[sp_ReportDetailsByDay]    Script Date: 11/17/2019 11:15:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ReportDetailsByDay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ReportDetailsByDay]
GO

/****** Object:  StoredProcedure [dbo].[sp_ReportDetailsByDay]    Script Date: 11/17/2019 11:15:26 AM ******/
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
	@Billable bit = NULL
AS
BEGIN
	SET NOCOUNT ON;

	WITH CTE AS(
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
	SELECT StartDay, SUM(RoundedHours) AS Hrs, SUM(RoundedHours * @Rate) AS Amt
	FROM CTE
	GROUP BY StartDay
END
GO";
            migrationBuilder.Sql(sp);
        }
    }
}