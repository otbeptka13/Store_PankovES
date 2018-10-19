SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 create procedure [lk].[AddNowWatching] @goodId bigint
	 as
	 begin
	 insert into lk.WatchedGood
	 (
	     goodId
	   , created
	 )
	 values
	 (  @goodId         -- goodId - bigint
	   , getdate() -- created - datetime
	 )
	 end
GO
