CREATE TABLE [lk].[Settings]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[name] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[value] [varchar] (2000) COLLATE Cyrillic_General_CI_AS NULL
)
GO
ALTER TABLE [lk].[Settings] ADD CONSTRAINT [PK__Settings__3213E83F098BAEFC] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_Settings_name] ON [lk].[Settings] ([name])
GO
