	--5
	select  Format(ArrivalDate,'yyyy-MM-dd') AS ArrivalDate,AdultsCount,ChildrenCount  from Bookings as b
	join Rooms as r on b.RoomId = r.Id
	order by r.Price desc, ArrivalDate asc