CREATE TABLE [employer_info].[EmploymentStatus]
(
	[EmpRef] VARCHAR(50) NOT NULL, 
    [Employed] BIT NOT NULL, 
    [Nino] VARCHAR(50) NULL, 
    [FromDate] DATETIME NULL, 
    [ToDate] DATETIME NULL
)
