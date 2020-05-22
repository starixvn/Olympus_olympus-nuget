CREATE PROCEDURE [dbo].JacobReturnTableAndOutput @MyOutput INT OUTPUT
AS
     SET @MyOutput = 15;
     SELECT 1 AS MyColumn;
GO

ALTER PROCEDURE [dbo].JacobReturnOutputAndRandomTable @MyOutput INT = 0 OUTPUT
AS
     
     IF @MyOutput = 1
         BEGIN
             SELECT 1 AS MyColumn;
         END;
     IF @MyOutput = 2
         BEGIN
             SELECT 1 AS MyColumn;
             SELECT 2 AS YourColumn;
         END;

	SET @MyOutput = @MyOutput + 10
GO

CREATE PROCEDURE [dbo].JacobReturn2TableAndOutput @MyOutput INT OUTPUT
AS
     SET @MyOutput = 15;
     SELECT 1 AS MyColumn;
     SELECT 2 AS YourColumn;
GO
GRANT EXECUTE ON dbo.JacobReturn2TableAndOutput TO bodbOWPNet;
GO
GRANT EXECUTE ON dbo.JacobReturnOutputAndRandomTable TO bodbOWPNet;
GO
GRANT EXECUTE ON dbo.JacobReturnTableAndOutput TO bodbOWPNet;
GO