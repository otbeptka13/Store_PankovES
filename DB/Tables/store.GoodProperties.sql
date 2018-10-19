CREATE TABLE [store].[GoodProperties]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[goodId] [bigint] NOT NULL,
[name] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[value] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
ALTER TABLE [store].[GoodProperties] ADD CONSTRAINT [PK__GoodProp__3213E83FA72717D7] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_GoodProperty_GoodIdName] ON [store].[GoodProperties] ([goodId], [name])
GO
ALTER TABLE [store].[GoodProperties] ADD CONSTRAINT [FK__GoodPrope__goodI__3EDC53F0] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id])
GO
