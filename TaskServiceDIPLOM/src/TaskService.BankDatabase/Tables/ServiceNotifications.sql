CREATE TABLE [dbo].[Service_MailSettings]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[From]			  NVARCHAR (30)  NOT NULL,
	[To]			  NVARCHAR (30)	 NOT NULL,
	[Subject]		  NVARCHAR (20)	 NOT NULL,
	[SmtpHost]		  NVARCHAR (50)  NOT NULL,
	[ModifiedBy]      NVARCHAR (50)  NOT NULL,
    [AuthoriziedBy]   NVARCHAR (50)  NOT NULL,
)
