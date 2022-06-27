CREATE TABLE [dbo].[UserLogins] (
    [UserLoginID]  SMALLINT     IDENTITY (1, 1) NOT NULL,
    [UserLogin]    VARCHAR (50) NOT NULL,
    [UserPassword] VARCHAR (20) NOT NULL,
    CONSTRAINT [pk_UL_UserLoginID] PRIMARY KEY CLUSTERED ([UserLoginID] ASC)
);

