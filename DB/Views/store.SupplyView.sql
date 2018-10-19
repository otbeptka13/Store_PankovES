SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE view [store].[SupplyView]
as  
select s.id, s.providerId, p.name, p.email, p.phone, s.shopId,  sh.adress, sh.timeOpen, sh.timeClose, sh.phone shopPhone,
	    s.goodId,  g.name goodName, s.count, g.typeId, g.price,g.info goodInfo, gt.name typeName, gt.info
from store.Supply s
inner join store.providers p on s.providerId = p.id
inner join store.Shops sh on s.shopId = sh.id
inner join store.Goods g on s.goodId = g.id
inner join store.GoodTypes gt on g.typeId = gt.id 

GO
