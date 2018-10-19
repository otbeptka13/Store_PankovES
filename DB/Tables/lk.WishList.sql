CREATE TABLE [lk].[WishList]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[userId] [bigint] NOT NULL,
[goodId] [bigint] NOT NULL
)
GO
ALTER TABLE [lk].[WishList] ADD CONSTRAINT [PK__WishList__3213E83FCD6AEF04] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_Wish] ON [lk].[WishList] ([goodId], [userId])
GO
ALTER TABLE [lk].[WishList] ADD CONSTRAINT [FK__WishList__userId__41B8C09B] FOREIGN KEY ([userId]) REFERENCES [lk].[Users] ([id])
GO
ALTER TABLE [lk].[WishList] ADD CONSTRAINT [FK__WishList__goodId__42ACE4D4] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id])
GO
