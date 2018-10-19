CREATE TABLE [lk].[UserRoles]
(
[userId] [bigint] NOT NULL,
[roleId] [bigint] NOT NULL
)
GO
ALTER TABLE [lk].[UserRoles] ADD CONSTRAINT [PK__UserRole__7743989DF7010A8B] PRIMARY KEY CLUSTERED  ([userId], [roleId])
GO
ALTER TABLE [lk].[UserRoles] ADD CONSTRAINT [FK__UserRoles__roleI__2610A626] FOREIGN KEY ([roleId]) REFERENCES [lk].[Roles] ([id])
GO
ALTER TABLE [lk].[UserRoles] ADD CONSTRAINT [FK__UserRoles__userI__251C81ED] FOREIGN KEY ([userId]) REFERENCES [lk].[Users] ([id])
GO
