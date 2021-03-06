/*
 Run this script: Database will be modified
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL Serializable
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Users]'
GO
CREATE TABLE [dbo].[Users]
(
[UserId] [int] NOT NULL IDENTITY(1, 1),
[UserName] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[FullName] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
[Email] [nvarchar] (max) COLLATE Cyrillic_General_CI_AS NULL,
[Password] [nvarchar] (max) COLLATE Cyrillic_General_CI_AS NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK__Users__1788CC4C126B7EA7] on [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK__Users__1788CC4C126B7EA7] PRIMARY KEY CLUSTERED  ([UserId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ__Users__C9F28456B6AB9A61] UNIQUE NONCLUSTERED  ([UserName])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[ConnectionInfoes]'
GO
CREATE TABLE [dbo].[ConnectionInfoes]
(
[ConnectionId] [nvarchar] (max) COLLATE Cyrillic_General_CI_AS NOT NULL,
[UserName] [nvarchar] (max) COLLATE Cyrillic_General_CI_AS NOT NULL,
[UserId] [int] NOT NULL,
[Date] [datetime] NOT NULL,
[Status] [int] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MessageLogs]'
GO
CREATE TABLE [dbo].[MessageLogs]
(
[MessageId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[Content] [nvarchar] (max) COLLATE Cyrillic_General_CI_AS NOT NULL,
[MessageDate] [datetime] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK__MessageL__C87C0C9C3DFEEDB0] on [dbo].[MessageLogs]'
GO
ALTER TABLE [dbo].[MessageLogs] ADD CONSTRAINT [PK__MessageL__C87C0C9C3DFEEDB0] PRIMARY KEY CLUSTERED  ([MessageId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[ConnectionInfoes]'
GO
ALTER TABLE [dbo].[ConnectionInfoes] ADD CONSTRAINT [FK__Connectio__UserI__47DBAE45] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[MessageLogs]'
GO
ALTER TABLE [dbo].[MessageLogs] ADD CONSTRAINT [FK__MessageLo__UserI__403A8C7D] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
-- This statement writes to the SQL Server Log so SQL Monitor can show this deployment.
IF HAS_PERMS_BY_NAME(N'sys.xp_logevent', N'OBJECT', N'EXECUTE') = 1
BEGIN
    DECLARE @databaseName AS nvarchar(2048), @eventMessage AS nvarchar(2048)
    SET @databaseName = REPLACE(REPLACE(DB_NAME(), N'\', N'\\'), N'"', N'\"')
    SET @eventMessage = N'Redgate SQL Compare: { "deployment": { "description": "Redgate SQL Compare deployed to ' + @databaseName + N'", "database": "' + @databaseName + N'" }}'
    EXECUTE sys.xp_logevent 55000, @eventMessage
END
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END
GO
