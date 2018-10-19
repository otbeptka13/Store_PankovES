CREATE TABLE [store].[ShopWorkers]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[shopId] [bigint] NOT NULL,
[workerId] [bigint] NOT NULL,
[hireDate] [datetime] NOT NULL
)
GO
ALTER TABLE [store].[ShopWorkers] ADD CONSTRAINT [PK__ShopWork__3213E83F4DE61D2A] PRIMARY KEY CLUSTERED  ([id])
GO
ALTER TABLE [store].[ShopWorkers] ADD CONSTRAINT [FK_shopId] FOREIGN KEY ([shopId]) REFERENCES [store].[Shops] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [store].[ShopWorkers] ADD CONSTRAINT [FK_workerId] FOREIGN KEY ([workerId]) REFERENCES [store].[Employees] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
