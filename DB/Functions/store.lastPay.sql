SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [store].[lastPay]()
RETURNS datetime 
AS 
-- Returns the stock level for the product.
BEGIN
    declare  @lastPayment datetime = (select max(created)
									from store.Orders )
    return @lastPayment	   
END;
GO
