CREATE TABLE [dbo].[EmploymentStatus]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[EmpRef] VARCHAR(50) NOT NULL, 
    [Employed] BIT NOT NULL, 
    [Nino] VARCHAR(50) NULL, 
    [FromDate] DATETIME NULL, 
    [ToDate] DATETIME NULL, 
    [HttpStatusCode] SMALLINT NULL,
	PRIMARY KEY(Id)
)
