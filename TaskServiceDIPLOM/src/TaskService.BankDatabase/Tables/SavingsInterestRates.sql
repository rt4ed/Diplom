CREATE TABLE [dbo].[SavingsInterestRates] (
    [InterestSavingRatesID]    TINYINT        IDENTITY (1, 1) NOT NULL,
    [InterestRatesValue]       NUMERIC (9, 9) NOT NULL,
    [InterestRatesDescription] VARCHAR (20)   NOT NULL,
    CONSTRAINT [pk_SIR_InterestSavingRatesID] PRIMARY KEY CLUSTERED ([InterestSavingRatesID] ASC)
);

