CREATE TABLE [store].[Goods]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[name] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[typeId] [bigint] NOT NULL,
[price] [decimal] (10, 2) NOT NULL,
[info] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[discount] [smallint] NOT NULL CONSTRAINT [DF__Goods__discount__2EA5EC27] DEFAULT ((0)),
[fullInfo] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[brandId] [bigint] NULL
)
GO
ALTER TABLE [store].[Goods] ADD CONSTRAINT [PK__Goods__3213E83FAB7D77A2] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE NONCLUSTERED INDEX [IX_FeedBacks_goodId] ON [store].[Goods] ([id])
GO
ALTER TABLE [store].[Goods] ADD CONSTRAINT [FK_GoodsId] FOREIGN KEY ([typeId]) REFERENCES [store].[GoodTypes] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [store].[Goods] ADD CONSTRAINT [FK__Goods__brandId__725BF7F6] FOREIGN KEY ([brandId]) REFERENCES [store].[GoodBrands] ([id])
GO
