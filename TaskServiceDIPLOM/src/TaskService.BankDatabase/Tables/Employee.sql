CREATE TABLE [dbo].[Employee] (
    [EmployeeID]            INT          IDENTITY (1, 1) NOT NULL,
    [EmployeeFirstName]     VARCHAR (25) NOT NULL,
    [EmployeeMiddleInitial] CHAR (1)     NULL,
    [EmployeeLastName]      VARCHAR (25) NULL,
    [EmployeeisManager]     BIT          NULL,
    CONSTRAINT [pk_E_EmployeeID] PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
);

