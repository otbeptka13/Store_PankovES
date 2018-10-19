SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [store].[CreateNewUser]
	@f [nvarchar](50),
	@i [nvarchar](50),
	@o [nvarchar](50) ,
	@phone [nvarchar](15),
	@email [nvarchar](50),
	@login [nvarchar](50),
	@password [varchar](50),
	@userType [bigint],
	@shopId  [bigint]
	as
	begin
	insert into store.Employees
	(
	    f
	  , i
	  , o
	  , phone
	  , email
	  , login
	  , passwordHash
	  , userType
	)
	select  @f
	  , @i
	  , @o
	  , @phone
	  , @email
	  , @login
	  , HashBytes('MD5', @password)
	  , @userType
	  
	  declare @id bigint = scope_identity()
	  insert into store.ShopWorkers
	  (
	      shopId
	    , workerId
	    , hireDate
	  )
	  values
	  (   @shopId         -- shopId - bigint
	    , @id         -- workerId - bigint
	    , getdate() -- hireDate - datetime
	  )	
	  end
GO
