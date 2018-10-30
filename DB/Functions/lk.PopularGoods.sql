SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 create function [lk].[PopularGoods]()
	 returns table 
	 as
	 return (select g.id
                  , g.name
                  , g.typeId
                  , g.price
                  , g.info
                  , g.imageUrl
                  , g.discount
                  , g.typeName
                  , g.typeInfo from store.GoodsView g
					inner join ( select top 10  count(w.id) countWatching, w.goodId 
									from lk.WatchedGood w
									group by goodId 
									order by count(w.id) desc
								 ) t on  g.id = t.goodId
			)
GO
