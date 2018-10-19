CREATE TABLE [store].[Shops]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[adress] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[timeOpen] [time] (1) NOT NULL,
[timeClose] [time] (1) NOT NULL,
[phone] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [store].[Shops] ADD CONSTRAINT [PK__Shops__3213E83F92932F74] PRIMARY KEY CLUSTERED  ([id])
GO
