USE [SFA-DAS-HMRC-API-Stub-Database]
GO

INSERT INTO [dbo].[AuthRecords]
           ([GatewayId]
           ,[ClientId]
           ,[AccessToken]
           ,[RefreshToken]
           ,[CreatedAt]
           ,[RefreshedAt]
           ,[Scope]
           ,[IsPrivileged]
           ,[ExpiresIn])
     SELECT
           'dgb1'
           ,'123456'
           ,'123456'
           ,'123456'
           ,'2019-01-01'
           ,'2019-12-31'
           ,'read:apprenticeship-levy'
           ,1
           ,3600
GO


