
CREATE PROCEDURE sp_Delete_Errors
AS
delete FROM LoginErrorLog
WHERE  ErrorTime BETWEEN DATEADD(hh, -1, GETDATE()) AND GETDATE();
