SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [lk].[RegisterUser] 
	@email [nvarchar](256),
	@passwordHash [nvarchar](256),
	@phoneNumber [nvarchar](128),
	@name [nvarchar](128),
	@userId bigint output,
	@message nvarchar(1000) output
    as
	begin
		if (exists(select 1 
			   from lk.Users
			   where email = @email))
		begin
			select @userId = null, @message = N'Данный email уже зарегистрирован'
			return
		end
		insert into lk.Users
		(
			email
		  , emailConfirmed
		  , passwordHash
		  , phoneNumber
		  , dateRegistration
		  , name
		)
		select   
		  @email       -- email - nvarchar(256)
		  , 0      -- emailConfirmed - bit
		  , @passwordHash       -- passwordHash - nvarchar(256)
		  , @phoneNumber       -- phoneNumber - nvarchar(128)
		  , getdate() -- dateRegistration - datetime
		  , @name

		set @userId = scope_identity()

	end

GO
