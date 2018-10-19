SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [store].[DeteleUser]  @userId bigint
as
delete store.Employees
where id = @userId
GO
