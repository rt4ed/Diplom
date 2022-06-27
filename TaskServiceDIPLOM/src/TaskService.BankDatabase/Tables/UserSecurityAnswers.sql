CREATE TABLE [dbo].[UserSecurityAnswers] (
    [UserLoginID]            SMALLINT     IDENTITY (1, 1) NOT NULL,
    [UserSecurityAnswers]    VARCHAR (25) NOT NULL,
    [UserSecurityQuestionID] TINYINT      NOT NULL,
    CONSTRAINT [pk_USA_UserLoginID] PRIMARY KEY CLUSTERED ([UserLoginID] ASC),
    CONSTRAINT [fk_UL_UserLoginID] FOREIGN KEY ([UserLoginID]) REFERENCES [dbo].[UserLogins] ([UserLoginID]),
    CONSTRAINT [fk_USQ_UserSecurityQuestionID] FOREIGN KEY ([UserSecurityQuestionID]) REFERENCES [dbo].[UserSecurityQuestions] ([UserSecurityQuestionID])
);

