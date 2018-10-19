CREATE TABLE [store].[Supply]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[providerId] [bigint] NOT NULL,
[shopId] [bigint] NOT NULL,
[goodId] [bigint] NOT NULL,
[count] [decimal] (5, 3) NULL,
[supplyPriceOne] [decimal] (10, 2) NOT NULL,
[created] [datetime] NOT NULL,
[isAddedInStore] [bit] NULL
)
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE trigger [store].[trNewGoodCapacityFromSupply] on [store].[Supply]
after insert
as
begin
update c
set [count] = c.[count] + i.[count]
from  store.Capacity c
inner join inserted i  on i.goodId = c.goodId  and i.shopId = c.shopId

update s
set isAddedInStore = 1
from  [store].[Supply] s
inner join inserted i  on i.id = s.id  
end
GO
ALTER TABLE [store].[Supply] ADD CONSTRAINT [PK__Supply__3213E83FDBCF80C0] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE NONCLUSTERED INDEX [supplyCreatedIndex] ON [store].[Supply] ([created])
GO
ALTER TABLE [store].[Supply] ADD CONSTRAINT [FK_goodId] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [store].[Supply] ADD CONSTRAINT [FK_providerId] FOREIGN KEY ([providerId]) REFERENCES [store].[Providers] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [store].[Supply] ADD CONSTRAINT [FK_shopSupply] FOREIGN KEY ([shopId]) REFERENCES [store].[Shops] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
