CREATE TABLE [lk].[Orders]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[transactionNumber] [uniqueidentifier] NOT NULL,
[summOrder] [decimal] (18, 2) NULL,
[userId] [bigint] NOT NULL,
[datePay] [datetime] NOT NULL CONSTRAINT [DF__Orders__datePay__6E8B6712] DEFAULT (getdate())
)
GO
ALTER TABLE [lk].[Orders] ADD CONSTRAINT [PK__Orders__3213E83FC1B974E9] PRIMARY KEY CLUSTERED  ([id])
GO
ALTER TABLE [lk].[Orders] ADD CONSTRAINT [FK__Orders__userId__6CA31EA0] FOREIGN KEY ([userId]) REFERENCES [lk].[Users] ([id])
GO
