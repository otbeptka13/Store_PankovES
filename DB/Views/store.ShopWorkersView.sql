SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE view [store].[ShopWorkersView]
as  
select sw.id,sw.shopId, s.adress, s.timeOpen, s.timeClose, s.phone shopPhone,  sw.workerid, e.f, e.i, e.o, e.phone, e.email, e.login, e.passwordHash
,ut.id userTypeId,ut.name userTypeName
from store.ShopWorkers sw
inner join store.Employees e on sw.workerId = e.id 
inner join store.Shops s on sw.shopId = s.id
inner join store.UserTypes ut on e.userType = ut.id
GO
