CREATE TABLE [dbo].[EmployerReference]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[EmpRef] varchar(50) NOT NULL, 
    [Data] VARCHAR(MAX) NOT NULL 
)
