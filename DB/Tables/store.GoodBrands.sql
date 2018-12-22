CREATE TABLE [store].[GoodBrands]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
ALTER TABLE [store].[GoodBrands] ADD CONSTRAINT [PK__GoodBran__3213E83FBCDE58D4] PRIMARY KEY CLUSTERED  ([id])
GO
