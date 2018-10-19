CREATE TABLE [store].[Orders]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[sellerId] [bigint] NOT NULL,
[shopId] [bigint] NOT NULL,
[price] [decimal] (10, 2) NOT NULL,
[typePay] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[created] [datetime] NOT NULL
)
GO
ALTER TABLE [store].[Orders] ADD CONSTRAINT [PK__Orders__3213E83F0781573F] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [orderCreatedDAteIndex] ON [store].[Orders] ([created])
GO
ALTER TABLE [store].[Orders] ADD CONSTRAINT [FK_seller_Id] FOREIGN KEY ([sellerId]) REFERENCES [store].[Employees] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [store].[Orders] ADD CONSTRAINT [FK_shop_Id] FOREIGN KEY ([shopId]) REFERENCES [store].[Shops] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
