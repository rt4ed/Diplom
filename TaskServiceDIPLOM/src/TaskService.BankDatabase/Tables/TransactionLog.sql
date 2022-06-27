CREATE TABLE [dbo].[TransactionLog] (
    [TransactionID]     INT      IDENTITY (1, 1) NOT NULL,
    [TransactionDate]   DATETIME NOT NULL,
    [TransactionTypeID] TINYINT  NOT NULL,
    [TransactionAmount] MONEY    NOT NULL,
    [NewBalance]        MONEY    NOT NULL,
    [AccountID]         INT      NOT NULL,
    [CustomerID]        INT      NOT NULL,
    [EmployeeID]        INT      NOT NULL,
    [UserLoginID]       SMALLINT NOT NULL,
    CONSTRAINT [pk_TL_TransactionID] PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    CONSTRAINT [fk_A_TL_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [fk_C_TL_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]),
    CONSTRAINT [fk_E_TL_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID]),
    CONSTRAINT [fk_TT_TL_TransactionTypeID] FOREIGN KEY ([TransactionTypeID]) REFERENCES [dbo].[TransactionType] ([TransactionTypeID]),
    CONSTRAINT [fk_UL_TL_UserLoginID] FOREIGN KEY ([UserLoginID]) REFERENCES [dbo].[UserLogins] ([UserLoginID])
);

