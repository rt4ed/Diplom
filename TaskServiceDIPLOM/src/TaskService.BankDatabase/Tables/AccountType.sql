CREATE TABLE [dbo].[AccountType] (
    [AccountTypeID]          TINYINT      IDENTITY (1, 1) NOT NULL,
    [AccountTypeDescription] VARCHAR (30) NOT NULL,
    CONSTRAINT [pk_AT_AccountTypeID] PRIMARY KEY CLUSTERED ([AccountTypeID] ASC)
);

