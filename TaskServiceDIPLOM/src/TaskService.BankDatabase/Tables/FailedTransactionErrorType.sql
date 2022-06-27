CREATE TABLE [dbo].[FailedTransactionErrorType] (
    [FailedTransactionErrorTypeID]          TINYINT      IDENTITY (1, 1) NOT NULL,
    [FailedTransactionErrorTypeDescription] VARCHAR (50) NOT NULL,
    CONSTRAINT [pk_FTET_FailedTransactionErrorTypeID] PRIMARY KEY CLUSTERED ([FailedTransactionErrorTypeID] ASC)
);

