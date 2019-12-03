USE [SFA-DAS-HMRC-API-Stub-Database]
GO

INSERT INTO [dbo].[EmployerReference]
           ([EmpRef]
           ,[Links]
           ,[EmployerName])
     VALUES
           ('001/CT00019'
           ,'
    "_links": {
        "self": {
            "href": "/epaye/001%2ACT00019"
        },
        "declarations": {
            "href": "/epaye/001%2ACT00019/declarations"
        },
        "fractions": {
            "href": "/epaye/001%2ACT00019/fractions"
        },
        "employment-check": {
            "href": "/epaye/001%2ACT00019/employed"
        }
    }'
           ,'ctuser19')
GO


