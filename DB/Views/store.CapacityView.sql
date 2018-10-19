SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE view [store].[CapacityView]
as  
select     g.name goodName, c.[count],c.shopId, sh.adress, sh.timeOpen, sh.timeClose, sh.phone shopPhone,
           g.id goodId,g.typeId, g.price goodPrice,g.info goodInfo, gt.name typeName, gt.info
		   
from store.Capacity c
inner join store.Shops sh on c.shopId = sh.id
inner join store.Goods g on c.goodId = g.id
inner join store.GoodTypes gt on g.typeId = gt.id 



GO
