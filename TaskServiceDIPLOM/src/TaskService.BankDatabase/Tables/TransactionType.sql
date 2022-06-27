CREATE TABLE [dbo].[TransactionType] (
    [TransactionTypeID]          TINYINT      IDENTITY (1, 1) NOT NULL,
    [TransactionTypeName]        CHAR (10)    NOT NULL,
    [TransactionTypeDescription] VARCHAR (50) NULL,
    [TransactionFeeAmount]       SMALLMONEY   NULL,
    CONSTRAINT [pk_TT_TransactionTypeID] PRIMARY KEY CLUSTERED ([TransactionTypeID] ASC)
);

