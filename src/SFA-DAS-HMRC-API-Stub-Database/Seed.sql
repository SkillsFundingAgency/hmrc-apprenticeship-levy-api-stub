/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

USE [SFA-DAS-HMRC-API-Stub-Database]
GO

INSERT INTO [dbo].[EmployerReference]
           ([EmpRef]
           ,[Data])
     VALUES
           ('001/DGB00001'
           ,'{"_links":{"self":{"href":"/epaye/001%2DGB00001"},"declarations":{"href":"/epaye/001%2DGB00001/declarations"},"fractions":{"href":"/epaye/001%2DGB00001/fractions"},"employment-check":{"href":"/epaye/001%2DGB00001/employed"}},"empref":"001/DGB00001","employer":{"name":{"nameLine1":"DGB1"}}}')
GO


INSERT INTO [dbo].[EmploymentStatus]
           ([EmpRef]
           ,[Employed]
           ,[Nino]
           ,[FromDate]
           ,[ToDate]
           ,[HttpStatusCode])
     VALUES
           ('001/DGB00001', 1, 'DB123456A', '2019-01-01', '2019-12-31', NULL)
GO


INSERT INTO [dbo].[GatewayUsers]
           ([EmpRef]
           ,[Name]
           ,[GatewayId]
           ,[Require2SV]
           ,[Password])
     SELECT
           '001/DGB00001'
           ,'Dan''s Softwarehouse'
           ,'DGB1'
           ,0
           ,'not-the-password'
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
           'DGB1'
           ,'123456'
           ,'123456'
           ,'123456'
           ,'2019-01-01'
           ,'2019-12-31'
           ,'read:apprenticeship-levy'
           ,1
           ,3600
GO


