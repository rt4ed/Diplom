CREATE TABLE [dbo].[UserSecurityQuestions] (
    [UserSecurityQuestionID] TINYINT      IDENTITY (1, 1) NOT NULL,
    [UserSecurityQuestion]   VARCHAR (50) NOT NULL,
    CONSTRAINT [pk_USQ_UserSecurityQuestionID] PRIMARY KEY CLUSTERED ([UserSecurityQuestionID] ASC)
);

