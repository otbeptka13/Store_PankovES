CREATE TABLE [store].[UserTypes]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[name] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [store].[UserTypes] ADD CONSTRAINT [PK__UserType__3213E83FEE2A5E62] PRIMARY KEY CLUSTERED  ([id])
GO
