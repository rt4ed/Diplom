CREATE TABLE [dbo].[FailedTransactionLog] (
    [FailedTransactionID]          INT      IDENTITY (1, 1) NOT NULL,
    [FailedTransactionErrorTypeID] TINYINT  NOT NULL,
    [FailedTransactionErrorTime]   DATETIME NULL,
    [FailedTransactionErrorXML]    XML      NULL,
    CONSTRAINT [pk_FTL_FailedTransactionID] PRIMARY KEY CLUSTERED ([FailedTransactionID] ASC),
    CONSTRAINT [fk_FTET_FailedTransactionErrorTypeID] FOREIGN KEY ([FailedTransactionErrorTypeID]) REFERENCES [dbo].[FailedTransactionErrorType] ([FailedTransactionErrorTypeID])
);

