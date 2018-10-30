SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 CREATE procedure [lk].[AddNowWatching] @goodId bigint, @userId bigint
	 as
	 begin
	 insert into lk.WatchedGood
	 (
	     goodId
	   , created
	   , userId
	 )
	 values
	 (  @goodId         -- goodId - bigint
	   , getdate() -- created - datetime
	   , @userId
	 )
	 end
GO
