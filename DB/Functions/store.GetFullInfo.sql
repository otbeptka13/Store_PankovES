SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
	  create function [store].[GetFullInfo](@goodId bigint)
	  returns varchar(max)
	  as
	  begin
	  declare @text varchar(max)
	  select @text= fullInfo from store.Goods
	  where id = @goodId
	  return @text
	  end
GO
