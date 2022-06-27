CREATE PROCEDURE [dbo].[Service_SetAsWorked]
	@TaskId INT,
	@LastWorkDate DATETIME
AS
BEGIN
	UPDATE [dbo].[ServiceTasks]
	SET LastWorkTime = @LastWorkDate
	WHERE TaskID = @TaskId
END