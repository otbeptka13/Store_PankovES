CREATE TABLE [lk].[Users]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[emailConfirmed] [bit] NULL,
[passwordHash] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[phoneNumber] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[dateRegistration] [datetime] NULL,
[generateToken] [uniqueidentifier] NULL,
[dateConfirmation] [datetime] NULL,
[name] [varchar] (200) COLLATE Cyrillic_General_CI_AS NULL
)
GO
ALTER TABLE [lk].[Users] ADD CONSTRAINT [PK__Users__3213E83F6D5303F0] PRIMARY KEY CLUSTERED  ([id])
GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_IND_Email] ON [lk].[Users] ([email])
GO
CREATE NONCLUSTERED INDEX [IX_IND_Token] ON [lk].[Users] ([generateToken])
GO
