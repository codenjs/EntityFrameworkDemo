CREATE TABLE [dbo].[Customers]
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [varchar](100) NOT NULL,
	[DiscountPercent] [int] NOT NULL
)

CREATE TABLE [dbo].[Products]
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [varchar](100) NOT NULL,
	[Price] [decimal] NOT NULL
)

CREATE TABLE [dbo].[Orders]
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CustomerId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[TotalCost] [decimal] NOT NULL
)
