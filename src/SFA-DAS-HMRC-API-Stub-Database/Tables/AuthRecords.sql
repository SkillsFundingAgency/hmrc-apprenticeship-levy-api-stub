CREATE TABLE [dbo].[AuthRecords]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GatewayId] VARCHAR(10) NOT NULL, 
    [ClientId] NVARCHAR(30) NOT NULL, 
    [AccessToken] VARCHAR(30) NOT NULL, 
    [RefreshToken] VARCHAR(30) NOT NULL, 
    [CreatedAt] DATETIME2 NOT NULL, 
    [RefreshedAt] DATETIME2 NOT NULL, 
    [Scope] VARCHAR(100) NOT NULL, 
    [IsPrivileged] BIT NOT NULL, 
    [ExpiresIn] INT NOT NULL
)

GO

CREATE INDEX [IX_AuthRecords_AccessToken] ON [dbo].[AuthRecords] ([AccessToken])
