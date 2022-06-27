CREATE TABLE [dbo].[LoginAccount] (
    [UserLoginID] SMALLINT NOT NULL,
    [AccountID]   INT      NOT NULL,
    CONSTRAINT [fk_A_Account] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [fk_UL_UserLogins] FOREIGN KEY ([UserLoginID]) REFERENCES [dbo].[UserLogins] ([UserLoginID])
);

