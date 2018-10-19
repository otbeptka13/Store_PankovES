SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create FUNCTION [store].[EmptyShopId]()
RETURNS   table
AS 
-- Returns the first name, last name, job title, and contact type for the specified contact.
return 
      (select  s.id
	   from store.Shops s
			left join (select shopId 
			      from store.Capacity
				  group by shopId) cap 
		    on  s.id = cap.shopId
	   where cap.shopId is null
	   group by s.id  
	   ) 


	  
GO
