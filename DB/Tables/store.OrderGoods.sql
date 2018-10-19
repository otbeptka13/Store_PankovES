CREATE TABLE [store].[OrderGoods]
(
[orderId] [bigint] NOT NULL,
[goodId] [bigint] NOT NULL,
[count] [decimal] (18, 2) NULL,
[priceOne] [decimal] (18, 2) NULL,
[summ] [decimal] (18, 2) NULL
)
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create trigger [store].[trNewOrderGoods] on [store].[OrderGoods]
after  insert
as
update c
set [count] = c.[count] - i.[count]
from  store.Capacity c
inner join inserted i  on i.goodId = c.goodId 
inner join store.Orders o on o.id = i.orderId and o.shopId = c.shopId
GO
ALTER TABLE [store].[OrderGoods] ADD CONSTRAINT [PK__OrderGoo__4E6BFA416C68C331] PRIMARY KEY CLUSTERED  ([orderId], [goodId])
GO
ALTER TABLE [store].[OrderGoods] ADD CONSTRAINT [FK_good_Id] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [store].[OrderGoods] ADD CONSTRAINT [FK_order_Id] FOREIGN KEY ([orderId]) REFERENCES [store].[Orders] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
