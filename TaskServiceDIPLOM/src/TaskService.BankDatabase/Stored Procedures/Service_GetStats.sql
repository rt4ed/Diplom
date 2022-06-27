CREATE PROCEDURE [dbo].[Service_GetStats]
	@Date DATE
AS
BEGIN
	SELECT 
		[TaskID],
		[TaskResult],
		[TaskStartTime],
		[TaskEndTime],
		[AffectedRows],
		[ErrorCount],
		[WarningCount], 
		[ProcessID]
	FROM 
		[dbo].[ServiceStats]
	WHERE CAST([TaskStartTime] AS DATE) = @Date
END
