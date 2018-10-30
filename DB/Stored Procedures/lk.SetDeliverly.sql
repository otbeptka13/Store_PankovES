SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [lk].[SetDeliverly] @packId bigint
as
begin
update lk.Basket
set status = 5
where status = 2 and packId = @packId
end
GO
