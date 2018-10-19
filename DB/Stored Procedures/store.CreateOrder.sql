SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [store].[CreateOrder](@sellerId bigint, @shopId bigint, @price decimal , @typePay varchar(50), @created datetime = null)
as
begin
insert into store.Orders (sellerId, shopId, price, typePay, created)
values (@sellerId, @shopId, @price, @typePay, isnull(@created,getdate()))
declare @idOrder bigint
set @idOrder =  scope_identity()
return @idOrder
end
GO
