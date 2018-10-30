CREATE TABLE [lk].[WatchedGood]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[goodId] [bigint] NOT NULL,
[created] [datetime] NOT NULL CONSTRAINT [DF__WatchedGo__creat__32767D0B] DEFAULT (getdate()),
[userId] [bigint] NOT NULL
)
GO
ALTER TABLE [lk].[WatchedGood] ADD CONSTRAINT [PK__WatchedG__3213E83F2A879168] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE NONCLUSTERED INDEX [IX_NowWied_GoodId] ON [lk].[WatchedGood] ([goodId])
GO
CREATE NONCLUSTERED INDEX [IX_NowWied_UserId] ON [lk].[WatchedGood] ([userId])
GO
ALTER TABLE [lk].[WatchedGood] ADD CONSTRAINT [FK__WatchedGo__userI__4A4E069C] FOREIGN KEY ([userId]) REFERENCES [lk].[Users] ([id])
GO
ALTER TABLE [lk].[WatchedGood] ADD CONSTRAINT [FK__WatchedGo__goodI__318258D2] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id])
GO
