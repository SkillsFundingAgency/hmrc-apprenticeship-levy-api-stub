CREATE PROCEDURE [employer_info].[GetEmploymentStatusByDate]
	@EmpRef varchar(50),
	@Nino varchar(50),
	@FromDate datetime,
	@ToDate datetime
AS
SELECT top 1 
	[EmpRef],
	1 as [Employed],	
	[Nino],	
	[FromDate],	
	[ToDate],
	[HttpStatusCode]
  FROM [employer_info].[EmploymentStatus]
	WHERE [EmpRef] = @EmpRef
	AND [Nino] = @Nino
	AND [FromDate] <= @FromDate
--	AND [ToDate] < @ToDate