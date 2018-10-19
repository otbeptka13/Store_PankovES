CREATE TABLE [store].[Capacity]
(
[shopId] [bigint] NOT NULL,
[goodId] [bigint] NOT NULL,
[count] [decimal] (10, 3) NULL
)
GO
ALTER TABLE [store].[Capacity] ADD CONSTRAINT [PK__Capacity__A3A6EDC006F8FD3A] PRIMARY KEY CLUSTERED  ([shopId], [goodId])
GO
ALTER TABLE [store].[Capacity] ADD CONSTRAINT [FK_goods_Id] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [store].[Capacity] ADD CONSTRAINT [FK_shops_Id] FOREIGN KEY ([shopId]) REFERENCES [store].[Shops] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
