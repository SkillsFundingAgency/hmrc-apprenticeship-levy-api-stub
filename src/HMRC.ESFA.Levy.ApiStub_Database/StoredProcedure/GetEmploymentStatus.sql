CREATE PROCEDURE [employer_info].[GetEmploymentStatus]
	@EmpRef varchar(50),
	@Nino varchar(50)
AS
SELECT top 1 
	[EmpRef],
	1 as [Employed],	
	[Nino],	
	[FromDate],	
	[ToDate],
	[HttpStatusCode]
  FROM [employer_info].[EmploymentStatus]
	WHERE empRef = @EmpRef
	AND Nino = @Nino