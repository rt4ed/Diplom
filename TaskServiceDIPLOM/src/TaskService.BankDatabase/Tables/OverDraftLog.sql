CREATE TABLE [dbo].[OverDraftLog] (
    [AccountID]               INT      IDENTITY (1, 1) NOT NULL,
    [OverDraftDate]           DATETIME NOT NULL,
    [OverDraftAmount]         MONEY    NOT NULL,
    [OverDraftTransactionXML] XML      NOT NULL,
    CONSTRAINT [Pk_ODL_AccountID] PRIMARY KEY CLUSTERED ([AccountID] ASC),
    CONSTRAINT [fk_A_ODL_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Account] ([AccountID])
);

