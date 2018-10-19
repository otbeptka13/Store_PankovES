CREATE TABLE [store].[GoodTypes]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[name] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[info] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[imageUrl] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [store].[GoodTypes] ADD CONSTRAINT [PK__GoodType__3213E83F581C6EC3] PRIMARY KEY CLUSTERED  ([id])
GO
