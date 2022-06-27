CREATE TABLE [dbo].[ServiceStats]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[TaskID] INT NOT NULL,
	[TaskResult] NVARCHAR(30) NOT NULL,
	[TaskStartTime]	DATETIME NOT NULL,
	[TaskEndTime] DATETIME NOT NULL,
	[AffectedRows] INT NULL DEFAULT 0,
	[ErrorCount] INT NOT NULL
	FOREIGN KEY ([TaskID]) REFERENCES [dbo].[ServiceTasks] ([TaskID]) DEFAULT 0, 
    [WarningCount] INT NOT NULL DEFAULT 0, 
    [ProcessID] SMALLINT NOT NULL
)
