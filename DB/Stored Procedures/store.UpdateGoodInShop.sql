SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 create procedure [store].[UpdateGoodInShop] (@goodId bigint, @shopId bigint, @count bigint)
 as
 begin
 if isnull(@goodId, 0) = 0 or isnull(@shopId, 0) = 0
	return
 
 update store.Capacity
 set count = @count
 where goodId = @goodId and shopId = @shopId
 end

GO
