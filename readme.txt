sql query 

select 
	p.uniqueName,
	w.*
from
	platform p 
outer apply(
	select 
		top 1 * 
	from 
		well w 
	where 
		w.platformid = p.id
	order by 
		w.updatedAt 
	desc) w
where 
	w.id is not null
