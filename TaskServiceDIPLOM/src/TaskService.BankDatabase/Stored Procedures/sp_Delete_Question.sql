
CREATE PROCEDURE sp_Delete_Question @UserLoginID SMALLINT
AS
DELETE UserSecurityQuestions
FROM UserSecurityQuestions uq
JOIN UserSecurityAnswers ua
ON ua.UserSecurityQuestionID = uq.UserSecurityQuestionID
JOIN UserLogins ul
ON ua.UserLoginID = ul.UserLoginID
WHERE ul.UserLoginID = @UserLoginID;
