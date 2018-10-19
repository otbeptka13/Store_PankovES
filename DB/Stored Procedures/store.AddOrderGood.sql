SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [store].[AddOrderGood](@orderId bigint, @goodId bigint, @count decimal(18,2), @priceOne decimal(18,2), @summ decimal(18,2))
as
begin
insert into store.OrderGoods (orderId, goodId, count, priceOne, summ)
values (@orderId, @goodId, @count, @priceOne, @summ)
end
GO
