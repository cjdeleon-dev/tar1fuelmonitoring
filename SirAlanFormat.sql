select a.vehicle,a.plateno,a.office,[Sep 2022 ltrs],[Sep 2022 amt],[Oct 2022 ltrs],[Oct 2022 amt],[Nov 2022 ltrs],[Nov 2022 amt],[Dec 2022 ltrs],[Dec 2022 amt],[Jan 2023 ltrs],[Jan 2023 amt]
from
	(SELECT vehicle,plateno,office,[Sep 2022 ltrs],[Oct 2022 ltrs],[Nov 2022 ltrs],[Dec 2022 ltrs],[Jan 2023 ltrs]
	FROM 
	(
		select vehicle,plateno,office,modateltrs,liters
		from
		(
			select isnull(v.vehicle,'OTHER')[vehicle],
			case when vehicleid=0 then vehicleplateno else v.plateno end [plateno],
			FORMAT (datefueled, 'MMM yyyy')+' ltrs' [modateltrs],sum(litersfueled)[liters],
			case when vehicleid=0 then ou.officedept else o.officedept end [office]
			from tblFueledVehicles fv left join tblVehicles v 
			on fv.vehicleid=v.id
			inner join tblUsers u on fv.userid=u.id
			left join tblOffices o on v.officeid=o.id
			left join tblOffices ou on u.officeid=ou.id
			where datefueled between @datefr and @dateto
			group by vehicle,case when vehicleid=0 then vehicleplateno else v.plateno end,
			FORMAT (datefueled, 'MMM yyyy')+' ltrs',
			case when vehicleid=0 then ou.officedept else o.officedept end
		) src
	
	) as P
	PIVOT
	(
		MAX(liters) for modateltrs IN ([Sep 2022 ltrs],[Oct 2022 ltrs],[Nov 2022 ltrs],[Dec 2022 ltrs],[Jan 2023 ltrs])
	) pivotLiters
)a
inner join
(select *
from
(
	SELECT vehicle,plateno,office,[Sep 2022 amt],[Oct 2022 amt],[Nov 2022 amt],[Dec 2022 amt],[Jan 2023 amt]
	FROM 
	(
		select vehicle,plateno,office,[modateamt],amounts
		from
		(
			select isnull(v.vehicle,'OTHER')[vehicle],
			case when vehicleid=0 then vehicleplateno else v.plateno end [plateno],
			FORMAT (datefueled, 'MMM yyyy')+' amt' [modateamt],sum(amtfueled)[amounts],
			case when vehicleid=0 then ou.officedept else o.officedept end [office]
			from tblFueledVehicles fv left join tblVehicles v 
			on fv.vehicleid=v.id
			inner join tblUsers u on fv.userid=u.id
			left join tblOffices o on v.officeid=o.id
			left join tblOffices ou on u.officeid=ou.id
			where datefueled between @datefr and @dateto
			group by vehicle,case when vehicleid=0 then vehicleplateno else v.plateno end,
			FORMAT (datefueled, 'MMM yyyy')+' amt',
			case when vehicleid=0 then ou.officedept else o.officedept end
		) src
	
	) as P
	PIVOT
	(
		MAX(amounts) for modateamt IN ([Sep 2022 amt],[Oct 2022 amt],[Nov 2022 amt],[Dec 2022 amt],[Jan 2023 amt])
	) pivotAmount
)x) as b
on a.vehicle=b.vehicle and a.plateno=b.plateno and a.office=b.office