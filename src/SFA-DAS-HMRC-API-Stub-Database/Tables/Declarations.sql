CREATE TABLE [dbo].[Declarations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmpRef] VARCHAR(50) NOT NULL, 
    [SubmissionTime] DATETIME2 NOT NULL, 
    [Data] VARCHAR(MAX) NOT NULL
)
