CREATE TABLE [store].[Employees]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[f] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[i] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[o] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[phone] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[login] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[passwordHash] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[userType] [bigint] NULL
)
GO
ALTER TABLE [store].[Employees] ADD CONSTRAINT [PK__Employee__3213E83F0E3E940D] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [employeeLoginIndex] ON [store].[Employees] ([login])
GO
ALTER TABLE [store].[Employees] ADD CONSTRAINT [FK_userType] FOREIGN KEY ([userType]) REFERENCES [store].[UserTypes] ([id])
GO
