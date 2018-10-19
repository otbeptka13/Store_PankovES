SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

create FUNCTION [store].[SupplySumByDate](@fromDate DateTime, @toDate DateTime = null) 
returns decimal(10,2) 
AS 
-- Returns the first name, last name, job title, and contact type for the specified contact.
begin
     declare @summ decimal(10,2) = 0
	  select @summ =  sum(supplyPriceOne*[count])
	   from store.Supply
	   where created >=@fromDate and created <=isnull(@toDate,getdate()) 
	   return @summ
   
end
GO
