/*
--Drop the test DB if it gets polluted during manual testing or debugging
--The next test run will recreate the DB

--ALTER DATABASE [TestEF] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE [TestEF]
*/

SELECT database_id FROM sys.databases WHERE Name = 'TestEF'

SELECT * FROM __MigrationHistory

DBCC CHECKIDENT ([Catalogs], NORESEED)
DBCC CHECKIDENT ([Categories], NORESEED)
DBCC CHECKIDENT ([Products], NORESEED)

SELECT * FROM [Catalogs]
SELECT * FROM [Categories]
SELECT * FROM [Products]

DECLARE @CatalogId INT SET @CatalogId = 1
SELECT * FROM [Catalogs] WHERE Id = @CatalogId
SELECT * FROM [Categories] WHERE CatalogId = @CatalogId
SELECT * FROM [Products] WHERE CategoryId IN (SELECT Id FROM [Categories] WHERE CatalogId = @CatalogId)
