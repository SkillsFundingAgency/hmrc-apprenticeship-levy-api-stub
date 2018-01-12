CREATE PROCEDURE [dbo].[GetResponse]
	@EmpRef varchar(50)
AS
	SELECT [Json] from [ResponseJson]
	WHERE empRef = @EmpRef
GO
