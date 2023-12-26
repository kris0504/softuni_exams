	select t.Id, t.Name, t.PhoneNumber from Tourists as t
	left join Bookings as b on t.Id=b.TouristId
	where b.TouristId is null
	order by Name