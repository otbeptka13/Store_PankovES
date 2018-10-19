CREATE TABLE [store].[Providers]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[name] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[phone] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [store].[Providers] ADD CONSTRAINT [PK__Provider__3213E83FA7565D93] PRIMARY KEY CLUSTERED  ([id])
GO
