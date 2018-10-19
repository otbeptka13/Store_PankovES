SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 create procedure [lk].[LeaveFeedback] @mark int, @message nvarchar(max), @goodId bigint, @userId bigint
   as
   begin
   insert into lk.FeedBacks
   (
       userId
     , mark
     , message
     , created
     , goodId
   )
   values
   (   @userId         -- userId - bigint
     , @mark         -- mark - int
     , @message       -- message - nvarchar(max)
     , getdate() -- created - date
     , @goodId       -- goodId - bigint
   )
   end
GO
