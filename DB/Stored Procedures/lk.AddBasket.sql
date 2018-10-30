SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [lk].[AddBasket] @userId bigint, @goodId bigint, @count decimal(18,3), @isFastPay bit,@packId bigint output
as
begin
set @packId = null

select @packId = max(packId)
from lk.Basket
where userId = @userId and status = 1 and isnull(isFastPay,0) = 0

if (@isFastPay = 1 or @packId is null)
begin
	select @packId = isnull(max(packId), 0) + 1
	from lk.Basket	
end

insert into lk.Basket
(
    packId
  , userId
  , created
  , summOne
  , count
  , status
  , goodId
  , isFastPay
)
select   
	@packId         -- packId - bigint
  , @userId         -- userId - bigint
  , getdate() -- created - datetime
  , g.price      -- summOne - decimal(18, 2)
  , @count      -- count - decimal(18, 2)
  , 1         -- status - tinyint
  , @goodId         -- goodId - bigint
  , @isFastPay
from store.Goods g
where g.id = @goodId
end




GO
