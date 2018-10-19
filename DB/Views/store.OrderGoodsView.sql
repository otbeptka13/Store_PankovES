SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [store].[OrderGoodsView]
AS  
SELECT     o.id, o.price orderPrice,o.typePay, o.created, g.id goodId,g.name goodName, g.typeId, g.price goodPrice,g.info goodInfo, gt.name typeName, gt.info,
           o.sellerId,e.f, e.i, e.o, e.phone, e.email, e.login, e.passwordHash,
		   o.shopId, sh.adress, sh.timeOpen, sh.timeClose, sh.phone shopPhone
		   
FROM store.OrderGoods od
inner join store.Orders o on od.orderId = o.id
inner join store.Employees e on o.sellerId = e.id
inner join store.Shops sh on o.shopId = sh.id
inner join store.Goods g on od.goodId = g.id
inner join store.GoodTypes gt on g.typeId = gt.id 

GO
