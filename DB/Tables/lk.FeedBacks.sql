CREATE TABLE [lk].[FeedBacks]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[userId] [bigint] NOT NULL,
[mark] [int] NOT NULL,
[message] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[created] [datetime] NULL,
[goodId] [bigint] NOT NULL
)
GO
ALTER TABLE [lk].[FeedBacks] ADD CONSTRAINT [PK__FeedBack__3213E83F94449660] PRIMARY KEY CLUSTERED  ([id])
GO
ALTER TABLE [lk].[FeedBacks] ADD CONSTRAINT [FK__FeedBacks__userI__39237A9A] FOREIGN KEY ([userId]) REFERENCES [lk].[Users] ([id])
GO
ALTER TABLE [lk].[FeedBacks] ADD CONSTRAINT [FK__FeedBacks__goodI__3B0BC30C] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id])
GO
