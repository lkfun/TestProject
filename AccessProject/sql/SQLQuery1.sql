--刚进入
	--59：	
	select* from events
	--212:	
	select filtername from filters order by fitername
	--874	
	select * from fiters where fitername='errors'
	--485	
	select datatime,type,cstr([module]) as szmodule,
	cstr(msg) as message,iif(blobs is null,'',yes) as Blobinfo from events order by datatime desc

	--584 
	(type like error)
	--533-有个定时刷新 也是这条
	select datatime,type,sctr([module]) as szmodule,cstr(msg) as message,iif(blob is null,'',yes) as blobinfo from events where datatime>'2002/08/23 09:14:34.765'
	order by datatime desc
--点击某条：
	--1189
	select* from events where datatime = '2005/08/23 08:47:46.734'
	--1463
	select * from events where datatime='2005/08/23 08:47:46.734'
	--1492
	select * from ealinfo where dtime = '2005/08/23 08:47:46.734'

