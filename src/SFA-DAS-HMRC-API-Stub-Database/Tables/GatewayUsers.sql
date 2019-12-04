CREATE TABLE [dbo].[GatewayUsers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmpRef] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [GatewayId] VARCHAR(10) NOT NULL, 
    [Require2SV] BIT NOT NULL, 
    [Password] VARCHAR(50) NOT NULL
)

GO

CREATE INDEX [IX_GatewayUsers_EmpRef] ON [dbo].[GatewayUsers] ([EmpRef])
