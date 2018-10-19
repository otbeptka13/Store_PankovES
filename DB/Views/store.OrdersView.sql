SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [store].[OrdersView]
AS  
SELECT o.id, o.price,o.typePay, o.created, o.sellerId,e.f, e.i, e.o, e.phone, e.email, e.login, e.passwordHash,
			 o.shopId, sh.adress, sh.timeOpen, sh.timeClose, sh.phone shopPhone
FROM store.Orders o
inner join store.Employees e on o.sellerId = e.id
inner join store.Shops sh on o.shopId = sh.id

GO
