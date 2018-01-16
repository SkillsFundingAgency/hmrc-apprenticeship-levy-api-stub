CREATE TABLE [employer_info].[EmploymentStatus]
(
	[EmpRef] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Employed] BIT NOT NULL, 
    [Nino] VARCHAR(50) NULL, 
    [FromDate] DATETIME NULL, 
    [ToDate] DATETIME NULL
)
