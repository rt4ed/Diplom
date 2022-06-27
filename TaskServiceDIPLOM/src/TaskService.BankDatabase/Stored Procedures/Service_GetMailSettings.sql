CREATE PROCEDURE [dbo].[Service_GetMailSettings]
AS
BEGIN
	SELECT
		[From],
		[To],
		[Subject],
		[SmtpHost]
	FROM 
		[dbo].[Service_MailSettings]
	WHERE 
		AuthoriziedBy IS NOT NULL
END