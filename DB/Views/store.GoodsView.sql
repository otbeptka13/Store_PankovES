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
     , gi.imageUrl
	 , g.discount
	 , gt.name as typeName
--	 , g.fullInfo
	 , gt.info as typeInfo 
	 , fb.mark
	 , g.brandId
	 , bg.name brandName
FROM store.Goods g with (nolock)
inner join store.GoodTypes gt with (nolock) on g.typeId = gt.id 
LEFT JOIN store.GoodBrands bg WITH(NOLOCK) ON bg.id =g.brandId
outer apply (select TOP 1 gim.imageUrl
			 from lk.GoodImages gim with (nolock)
			 where gim.goodId= g.id AND gim.isPrimary = 1 ) gi
outer apply (select convert(decimal(5,1),avg(convert(decimal(5,1),mark))) mark 
			 from lk.FeedBacks fb with (nolock)
			 where fb.goodId = g.id
			-- group by fb.mark
			 ) fb




GO
