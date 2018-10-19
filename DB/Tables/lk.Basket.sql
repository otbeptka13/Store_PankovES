CREATE TABLE [lk].[Basket]
(
[id] [bigint] NOT NULL IDENTITY(1, 1),
[packId] [bigint] NULL,
[userId] [bigint] NOT NULL,
[created] [datetime] NOT NULL CONSTRAINT [DF__Basket__created__44952D46] DEFAULT (getdate()),
[summOne] [decimal] (18, 2) NULL,
[count] [decimal] (18, 2) NOT NULL,
[summTotal] AS ([summOne]*[count]),
[status] [tinyint] NOT NULL CONSTRAINT [DF__Basket__status__4589517F] DEFAULT ((1)),
[goodId] [bigint] NOT NULL,
[transactionNumber] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[datePay] [datetime] NULL
)
GO
CREATE NONCLUSTERED INDEX [IX_Basket] ON [lk].[Basket] ([userId], [status], [packId])
GO
ALTER TABLE [lk].[Basket] ADD CONSTRAINT [FK__Basket__userId__467D75B8] FOREIGN KEY ([userId]) REFERENCES [lk].[Users] ([id])
GO
ALTER TABLE [lk].[Basket] ADD CONSTRAINT [FK__Basket__goodId__477199F1] FOREIGN KEY ([goodId]) REFERENCES [store].[Goods] ([id])
GO
