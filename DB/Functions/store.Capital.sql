SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [store].[Capital]()
RETURNS bigint 
AS 
-- Returns the stock level for the product.
BEGIN
    declare  @sumSale bigint = (select sum(price)
									from store.Orders)
	declare  @sumBuy bigint = (select sum(count*supplyPriceOne)
							    from store.Supply)
    return @sumSale	-@sumBuy   
END;
GO
