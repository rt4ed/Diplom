CREATE PROCEDURE [dbo].[Service_Clients_Import]
AS
BEGIN
	MERGE dbo.Customer as [t]
	USING
	(
		SELECT
			 [AccountCode]
			,[CustomerAddress1]
			,[CustomerAddress2]
			,[CustomerAddress3]
			,[BirthDate]
			,[CustomerFirstName]
			,[CustomerMiddleName]
			,[CustomerLastName]
			,[City]
			,[State]
			,[ZipCode]
			,[EmailAddress]
			,[CellPhone]
			,[Residency]
			,[KPP]
			,[OKATO]
			,CountryCode
			,[Office_Name1]
			,[Office_Name2]
			,[Office_Name3]
			,[IsClosed]
			,[CloseDate]
		FROM 
			dbo.Customer_TMP
	) as [s]
	ON [t].AccountCode = [s].AccountCode
	WHEN MATCHED THEN
		UPDATE SET
			 t.[AccountCode] = s.[AccountCode]
			,t.[CustomerAddress1] = s.[CustomerAddress1]
			,t.[CustomerAddress2] = s.[CustomerAddress2]
			,t.[CustomerAddress3] = s.[CustomerAddress3]
			,t.[BirthDate] = s.[BirthDate]
			,t.[CustomerFirstName] = s.[CustomerFirstName]
			,t.[CustomerMiddleName] = s.[CustomerMiddleName]
			,t.[CustomerLastName] = s.[CustomerLastName]
			,t.[City] = s.[City]
			,t.[State] = s.[State]
			,t.[ZipCode] = s.[ZipCode]
			,t.[EmailAddress] = s.[EmailAddress]
			,t.[CellPhone] = s.[CellPhone]
			,t.[Residency] = s.[Residency]
			,t.[KPP] = s.[KPP]
			,t.[OKATO] = s.[OKATO]
			,t.CountryCode = s.CountryCode
			,t.[Office_Name1] = s.[Office_Name1]
			,t.[Office_Name2] = s.[Office_Name2]
			,t.[Office_Name3] = s.[Office_Name3]
			,t.[IsClosed] = s.[IsClosed]
			,t.[CloseDate] = s.[CloseDate]
	WHEN NOT MATCHED THEN
		INSERT VALUES 
		(
			 [s].[AccountCode]
			,[s].[CustomerAddress1]
			,[s].[CustomerAddress2]
			,[s].[CustomerAddress3]
			,[s].[BirthDate]
			,[s].[CustomerFirstName]
			,[s].[CustomerMiddleName]
			,[s].[CustomerLastName]
			,[s].[City]
			,[s].[State]
			,[s].[ZipCode]
			,[s].[EmailAddress]
			,[s].[CellPhone]
			,[s].[Residency]
			,[s].[KPP]
			,[s].[OKATO]
			,[s].CountryCode
			,[s].[Office_Name1]
			,[s].[Office_Name2]
			,[s].[Office_Name3]
			,[s].[IsClosed]
			,[s].[CloseDate]
		);
END