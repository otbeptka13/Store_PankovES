SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
	CREATE procedure [lk].[ChangePassword]  @token uniqueidentifier, @newHashPassword nvarchar(1000),@userId bigint output
	as
	begin
	update lk.Users
	set generateToken = null, passwordHash = @newHashPassword, @userId = id
		where generateToken = @token
	end
GO
