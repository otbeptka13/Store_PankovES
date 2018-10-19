SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 CREATE function [lk].[GetNowWatching]()
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
					inner join ( select top 10  max(w.id) id, w.goodId 
									from lk.WatchedGood w
									group by w.goodId
									order by max(w.id) desc
								 ) t on  g.id = t.goodId
			)
GO
