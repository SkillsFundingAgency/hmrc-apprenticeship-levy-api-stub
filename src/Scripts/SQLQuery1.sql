USE [SFA-DAS-HMRC-API-Stub-Database]
GO

INSERT INTO [dbo].[EmploymentStatus]
           ([EmpRef]
           ,[Employed]
           ,[Nino]
           ,[FromDate]
           ,[ToDate]
           ,[HttpStatusCode])
     VALUES
           ('999/PY00003', 1, 'NM123456A', '2019-01-01', '2019-12-31', NULL)
GO


