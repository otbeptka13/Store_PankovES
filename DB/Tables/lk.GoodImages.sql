CREATE TABLE [lk].[GoodImages]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[goodId] [bigint] NOT NULL,
[imageUrl] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[isPrimary] [bit] NULL CONSTRAINT [DF_GoodImages_isPrimary] DEFAULT ((0))
)
GO
ALTER TABLE [lk].[GoodImages] ADD CONSTRAINT [PK__GoodImages_id] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE NONCLUSTERED INDEX [IX_GoodImages_goodId] ON [lk].[GoodImages] ([goodId])
GO
ALTER TABLE [lk].[GoodImages] ADD CONSTRAINT [FK_GoodImages_goodId] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id])
GO
