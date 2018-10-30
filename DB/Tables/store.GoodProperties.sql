CREATE TABLE [store].[GoodProperties]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[goodId] [bigint] NOT NULL,
[name] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[value] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
ALTER TABLE [store].[GoodProperties] ADD CONSTRAINT [PK__GoodProp__3213E83F2B38485C] PRIMARY KEY CLUSTERED  ([id])
GO
ALTER TABLE [store].[GoodProperties] ADD CONSTRAINT [FK__GoodPrope__goodI__52E34C9D] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id])
GO
