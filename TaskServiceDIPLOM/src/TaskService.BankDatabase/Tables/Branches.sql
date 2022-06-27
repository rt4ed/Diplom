CREATE TABLE [dbo].[Branches] (
    [BranchCode]  INT            NOT NULL,
    [BranchName]  NVARCHAR (4)   NOT NULL,
    [BankName]    NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([BranchCode] ASC)
);

