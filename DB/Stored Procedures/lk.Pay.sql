SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 CREATE procedure [lk].[Pay] @packId bigint, @userId bigint, @countInBasket int,  @payDate datetime, @transactionNumber varchar(50), @totalSumm decimal(18, 2)
 as
 begin 
 IF OBJECT_ID('tempdb..#basket') IS NOT NULL 
    DROP TABLE #basket
	create table #basket(id bigint not null primary key(id), summ decimal(18, 2) not null)
	
	insert into #basket
	select id, summTotal
	from lk.Basket
	where userId =@userId and packId = @packId and status = 1 
	
	if (@countInBasket != (select count(id) from #basket))
	begin
	 RAISERROR (N'Некорректная попытка оплаты.  Количество элементов не соответсвует ожидаемому', -- Message text.  
               16, -- Severity.  
               1 -- State.  
               );  
	return;
	end
	if (@totalSumm != (select sum(summ) from #basket))
	begin
	declare @mess nvarchar(4000) = N'Некорректная попытка оплаты. Сумма элементов не соответсвует ожидаемой. ' + convert(varchar(1000), @totalSumm) +'!=' +convert(varchar(1000),(select sum(summ) from #basket)) 
	 RAISERROR (@mess, -- Message text.  
               16, -- Severity.  
               1 -- State.  
               );  
	return;
	end
	if ( @totalSumm <= 0)
	begin
	 RAISERROR (N'Некорректная попытка оплаты. Отрицательная сумма', -- Message text.  
               16, -- Severity.  
               1 -- State.  
               );  
	return;
	END
    
	INSERT INTO lk.Orders
	(
	    transactionNumber,
	    summOrder,
	    userId,
	    datePay
	)
	VALUES
	(   @transactionNumber,     -- transactionNumber - uniqueidentifier
	    @totalSumm,     -- summOrder - decimal(18, 2)
	    @userId,        -- userId - bigint
	    GETDATE() -- datePay - datetime
	)
	DECLARE @orderId BIGINT = scope_identity()
	update lk.Basket
	set orderId = @orderId, status = 2
	where id in (select id from #basket)
 end


GO
