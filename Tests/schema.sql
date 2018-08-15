CREATE TABLE [dbo].[Catalogs]
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [varchar](100) NOT NULL
)

CREATE TABLE [dbo].[Categories]
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [varchar](100) NOT NULL,
	[CatalogId] [int] NOT NULL,
	[ParentCategoryId] [int] NULL
)

CREATE TABLE [dbo].[Products]
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [varchar](100) NOT NULL,
	[CategoryId] [int] NOT NULL
)
