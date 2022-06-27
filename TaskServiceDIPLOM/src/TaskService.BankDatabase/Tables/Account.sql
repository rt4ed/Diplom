CREATE TABLE [dbo].[Account] (
    [AccountID]             INT     IDENTITY (1, 1) NOT NULL,
    [CurrentBalance]        INT     NOT NULL,
    [AccountTypeID]         TINYINT NOT NULL,
    [AccountStatusTypeID]   TINYINT NOT NULL,
    [InterestSavingRatesID] TINYINT NOT NULL,
    CONSTRAINT [pk_A_AccounID] PRIMARY KEY CLUSTERED ([AccountID] ASC),
    FOREIGN KEY ([AccountTypeID]) REFERENCES [dbo].[AccountType] ([AccountTypeID]),
    CONSTRAINT [fk_AST_AccountStatusTypeID] FOREIGN KEY ([AccountStatusTypeID]) REFERENCES [dbo].[AccountStatusType] ([AccountStatusTypeID]),
    CONSTRAINT [fk_SIR_InterestSavingRatesID] FOREIGN KEY ([InterestSavingRatesID]) REFERENCES [dbo].[SavingsInterestRates] ([InterestSavingRatesID])
);

