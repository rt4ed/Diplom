CREATE TABLE [dbo].[AccountStatusType] (
    [AccountStatusTypeID]          TINYINT      IDENTITY (1, 1) NOT NULL,
    [AccountStatusTypeDescription] VARCHAR (30) NOT NULL,
    CONSTRAINT [pk_AST_AccountStatusTypeID] PRIMARY KEY CLUSTERED ([AccountStatusTypeID] ASC)
);

