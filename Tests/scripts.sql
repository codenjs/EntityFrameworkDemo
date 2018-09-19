/*
--Drop the test DB if it gets polluted during manual testing or debugging
--The next test run will recreate the DB

--ALTER DATABASE [TestEF] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE [TestEF]
*/

SELECT database_id FROM sys.databases WHERE Name = 'TestEF'

SELECT * FROM [Products]
SELECT * FROM [Customers]
SELECT * FROM [Orders]
