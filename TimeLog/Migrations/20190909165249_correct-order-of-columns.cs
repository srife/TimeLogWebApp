using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLog.Migrations
{
    public partial class correctorderofcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ActivityEntity
	DROP CONSTRAINT FK_ActivityEntity_Clients_ClientId
GO
ALTER TABLE dbo.Clients SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ActivityEntity
	DROP CONSTRAINT FK_ActivityEntity_Projects_ProjectId
GO
ALTER TABLE dbo.Projects SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ActivityEntity
	DROP CONSTRAINT FK_ActivityEntity_Locations_LocationId
GO
ALTER TABLE dbo.Locations SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ActivityEntity
	DROP CONSTRAINT FK_ActivityEntity_ActivityTypes_ActivityTypeId
GO
ALTER TABLE dbo.ActivityTypes SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_ActivityEntity
	(
	Id int NOT NULL IDENTITY (1, 1),
	RowVersion timestamp NULL,
	StartTime datetimeoffset(7) NOT NULL,
	EndTime datetimeoffset(7) NULL,
	ClientId int NULL,
	ProjectId int NULL,
	ActivityTypeId int NOT NULL,
	LocationId int NULL,
    Tasks nvarchar(MAX) NULL,
    Billable bit NOT NULL,
	InvoiceStatement nvarchar(MAX) NULL,
	ParentId int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_ActivityEntity SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_ActivityEntity ON
GO
IF EXISTS(SELECT * FROM dbo.ActivityEntity)
	 EXEC('INSERT INTO dbo.Tmp_ActivityEntity (Id, StartTime, EndTime, Tasks, Billable, ClientId, ActivityTypeId, ProjectId, LocationId, InvoiceStatement, ParentId)
		SELECT Id, StartTime, EndTime, Tasks, Billable, ClientId, ActivityTypeId, ProjectId, LocationId, InvoiceStatement, ParentId FROM dbo.ActivityEntity WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_ActivityEntity OFF
GO
ALTER TABLE dbo.ActivityEntity
	DROP CONSTRAINT FK_ActivityEntity_ActivityEntity_ParentId
GO
DROP TABLE dbo.ActivityEntity
GO
EXECUTE sp_rename N'dbo.Tmp_ActivityEntity', N'ActivityEntity', 'OBJECT'
GO
ALTER TABLE dbo.ActivityEntity ADD CONSTRAINT
	PK_ActivityEntity PRIMARY KEY CLUSTERED
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_ActivityEntity_ActivityTypeId ON dbo.ActivityEntity
	(
	ActivityTypeId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_ActivityEntity_ClientId ON dbo.ActivityEntity
	(
	ClientId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_ActivityEntity_LocationId ON dbo.ActivityEntity
	(
	LocationId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_ActivityEntity_ProjectId ON dbo.ActivityEntity
	(
	ProjectId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_ActivityEntity_ParentId ON dbo.ActivityEntity
	(
	ParentId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.ActivityEntity ADD CONSTRAINT
	FK_ActivityEntity_ActivityTypes_ActivityTypeId FOREIGN KEY
	(
	ActivityTypeId
	) REFERENCES dbo.ActivityTypes
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  CASCADE

GO
ALTER TABLE dbo.ActivityEntity ADD CONSTRAINT
	FK_ActivityEntity_Locations_LocationId FOREIGN KEY
	(
	LocationId
	) REFERENCES dbo.Locations
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION

GO
ALTER TABLE dbo.ActivityEntity ADD CONSTRAINT
	FK_ActivityEntity_Projects_ProjectId FOREIGN KEY
	(
	ProjectId
	) REFERENCES dbo.Projects
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION

GO
ALTER TABLE dbo.ActivityEntity ADD CONSTRAINT
	FK_ActivityEntity_Clients_ClientId FOREIGN KEY
	(
	ClientId
	) REFERENCES dbo.Clients
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION

GO
ALTER TABLE dbo.ActivityEntity ADD CONSTRAINT
	FK_ActivityEntity_ActivityEntity_ParentId FOREIGN KEY
	(
	ParentId
	) REFERENCES dbo.ActivityEntity
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION

GO
COMMIT

";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}