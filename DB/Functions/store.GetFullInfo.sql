SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
	  CREATE function [store].[GetFullInfo](@goodId bigint)
	  returns nvarchar(max)
	  as
	  begin
	  declare @text nvarchar(max)
	  select @text= fullInfo from store.Goods
	  where id = @goodId
	  return @text
	  end
GO
