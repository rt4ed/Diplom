-----------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Events] ON
GO

IF NOT EXISTS(SELECT 1 FROM [dbo].[Events])
BEGIN
    INSERT INTO [dbo].[Events] ([EventId], [Description])
    VALUES
        (0, 'Trace'),
        (1, 'UserAction')
END
GO

SET IDENTITY_INSERT [dbo].[Events] OFF
GO
-----------------------------------------------------------------

IF NOT EXISTS(SELECT 1 FROM [dbo].[Branches])
BEGIN
    INSERT INTO dbo.Branches
    VALUES
        (0, 'RU1', 'Moscow', NULL),
        (1, 'RU2', 'SaintPetersburg', NULL),
        (2, 'RU3', 'Kazan', NULL),
        (3, 'RU4', 'Izhevsk', NULL),
        (4, 'RU5', 'Novosibirsk', NULL)
END

-----------------------------------------------------------------

SET IDENTITY_INSERT [dbo].[Users] ON
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Users])
BEGIN
    INSERT INTO [dbo].[Users] (Id, Username, Privileges, PassHash)
    VALUES
        (-100, 'TaskService', 'ServicePrivelege', 'NO PASS')
END
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO

IF NOT EXISTS (SELECT 1 FROM dbo.TaskTypes)
BEGIN

    INSERT INTO dbo.TaskTypes ([Type])
    VALUES ('WebService'),
        ('StoredProcedure'),
        ('File')

END

-----------------------------------------------------------------

-- Task CBR RATES
IF NOT EXISTS(SELECT 1 FROM dbo.ServiceTasks WHERE TaskName = 'CurrentCBRRates')
BEGIN
    INSERT INTO dbo.ServiceTasks 
        (TaskID, [IsEnabled],[TaskType],[Branch],
        [TaskName],[LastWorkTime],[TaskStartTime],
        [TaskEndTime],[Dependency],[FilePath],
        [FieldsCount],[FieldsSeparator],[Params],
        [ModifiedBy],[AuthoriziedBy],[Description],[ManualStart])
    VALUES
        (0, 1, 1, null,
        'CurrentCBRRates', null, '2022-05-01 01:00:00',
        '2022-05-01 23:59:00', '', null,
        null, null, null,
        'Andrej', 'Andrej', 'CBR Rates', 0)
END
GO
-----------------------------------------------------------------

-- Task ED Banks

IF NOT EXISTS(SELECT 1 FROM dbo.ServiceTasks WHERE TaskName = 'EDBanks')
BEGIN
    INSERT INTO dbo.ServiceTasks 
        (TaskID, [IsEnabled],[TaskType],[Branch],
        [TaskName],[LastWorkTime],[TaskStartTime],
        [TaskEndTime],[Dependency],[FilePath],
        [FieldsCount],[FieldsSeparator],[Params],
        [ModifiedBy],[AuthoriziedBy],[Description],[ManualStart], [Url], [FileMask])
    VALUES
        (1, 1, 3, null,
        'EDBanks', null, '2022-05-01 01:00:00',
        '2022-05-01 23:59:00', '', 'C:\TEST',
        null, null, null,
        'Andrej', 'Andrej', 'BIC Dictionary', 0, 'https://www.cbr.ru/s/newbik', '*_ED807_full.xml')
END
GO
-----------------------------------------------------------------
-- Task Clients

IF NOT EXISTS(SELECT 1 FROM dbo.ServiceTasks WHERE TaskName = 'Clients')
BEGIN
    INSERT INTO dbo.ServiceTasks 
        (TaskID, [IsEnabled],[TaskType],[Branch],
        [TaskName],[LastWorkTime],[TaskStartTime],
        [TaskEndTime],[Dependency],[FilePath],
        [FieldsCount],[FieldsSeparator],[Params],
        [ModifiedBy],[AuthoriziedBy],[Description],[ManualStart], [Url], [FileMask])
    VALUES
        (1, 1, 3, null,
        'Clients', null, '2022-05-01 01:00:00',
        '2022-05-01 23:59:00', '', 'C:\TEST',
        22, ';', null,
        'Andrej', 'Andrej', 'Clients update', 0, '', 'Clients*.txt')
END
GO
-----------------------------------------------------------------