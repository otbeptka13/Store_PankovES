SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [store].[PaysByDate](@fromDate DateTime, @toDate DateTime = null)
RETURNS table  
AS 
-- Returns the first name, last name, job title, and contact type for the specified contact.
return
      (select  *
	   from store.OrdersView
	   where created >=@fromDate and created <=isnull(@toDate,getdate())  
	   ) 
   

GO
