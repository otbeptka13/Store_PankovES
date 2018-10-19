SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [store].[GoodsView]
AS  
SELECT g.id
     , g.name
     , g.typeId
     , g.price
     , g.info
     , g.imageUrl
	 , g.discount
	 , gt.name as typeName
--	 , g.fullInfo
	 , gt.info as typeInfo 
	 , fb.mark
FROM store.Goods g with (nolock)
inner join store.GoodTypes gt with (nolock) on g.typeId = gt.id 
outer apply (select convert(decimal(5,1),avg(convert(decimal(5,1),mark))) mark 
			 from lk.FeedBacks fb with (nolock)
			 where fb.goodId = g.id
			 group by fb.mark
			 ) fb

GO
