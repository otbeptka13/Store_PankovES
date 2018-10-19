SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
	create procedure [lk].[GenerateToken] @userId bigint, @token uniqueidentifier output
	as
	begin
	set @token = newid()
	update lk.Users
	set generateToken = @token
	where id = @userId
	end
GO
