select isnull(fv.vehicleplateno,v.plateno+' - '+m.name) vehicleplateno,
litersfueled,amtfueled,datefueled,isnull((case when right(o.officedept,11)='MAIN OFFICE' then 'MAIN OFFICE' else o.officedept end),oo.officedept)[officedept]
from tblFueledVehicles fv 
left join tblVehicles v
on fv.vehicleid=v.id
left join tblMake m
on v.makeid=m.id
left join tblOffices o
on v.officeid=o.id
left join tblUsers u
on fv.userid=u.id
left join tblOffices oo
on u.officeid=oo.id
where datefueled between '2022-09-15' and '2022-09-30'
order by datefueled