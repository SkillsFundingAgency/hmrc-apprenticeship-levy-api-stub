/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) *
  FROM [SFA-DAS-HMRC-API-Stub-Database].[dbo].[EmploymentStatus]


  SELECT TOP(1) [es].[Id], [es].[EmpRef], [es].[Employed], [es].[FromDate], [es].[HttpStatusCode], [es].[Nino], [es].[ToDate]
FROM [EmploymentStatus] AS [es]
WHERE (([es].[EmpRef] = '001/CT00019') AND ([es].[Nino] = 'CT000019A')) AND ((CONVERT(date, '2017-01-02') >= CONVERT(date, [es].[FromDate])) AND (CONVERT(date, '2099-10-09') < CONVERT(date, [es].[ToDate])))