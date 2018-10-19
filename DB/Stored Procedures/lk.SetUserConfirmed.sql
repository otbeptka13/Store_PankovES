SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
	CREATE procedure [lk].[SetUserConfirmed] @token uniqueidentifier, @userId bigint output
	as
	begin
	update lk.Users
	set generateToken = null, dateConfirmation = getdate(), emailConfirmed = 1, @userId = id
	where generateToken = @token

	end

GO
