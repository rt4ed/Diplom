CREATE TABLE [dbo].[LoginErrorLog] (
    [ErrorLogID]           INT      IDENTITY (1, 1) NOT NULL,
    [ErrorTime]            DATETIME NOT NULL,
    [FailedTransactionXML] XML      NULL,
    CONSTRAINT [pk_LEL_ErrorLogID] PRIMARY KEY CLUSTERED ([ErrorLogID] ASC)
);

