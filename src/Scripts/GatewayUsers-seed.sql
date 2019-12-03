USE [SFA-DAS-HMRC-API-Stub-Database]
GO

INSERT INTO [dbo].[GatewayUsers]
           ([EmpRef]
           ,[Name]
           ,[GatewayId]
           ,[Require2SV]
           ,[Password])
     SELECT
           '001/CT00019'
           ,'Dan''s Softwarehouse'
           ,'dgb1'
           ,0
           ,'password'
GO


