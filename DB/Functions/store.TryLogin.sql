SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE function [store].[TryLogin](@login varchar(50), @password varchar(50))
returns int 
as
begin
declare @res int = 0;
declare @p nvarchar(256)
select @p = HashBytes('MD5', @password)
select @res= max(id) 
	from store.Employees  au
	where login = @login and @p=au.PasswordHash 
		   
return @res
end
GO
