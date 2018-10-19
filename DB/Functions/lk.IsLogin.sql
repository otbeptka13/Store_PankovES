SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
	CREATE function [lk].[IsLogin] (@email varchar(100), @hashPassword nvarchar(1000))
	returns bigint
	as
	begin
		declare @userid bigint 

		select @userid = id
		from lk.Users
		where email = @email and passwordHash= @hashPassword

		return @userid			    
	 end
GO
