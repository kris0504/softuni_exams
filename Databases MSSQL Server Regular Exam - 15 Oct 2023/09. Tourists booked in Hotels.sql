	select h.Name, r.Price from Tourists as t
	join Bookings as b on t.Id=b.TouristId
	join Hotels as h on b.HotelId=h.Id
	join Rooms as r on b.RoomId = r.Id
	where t.Name not like '%ez'
	order by r.Price desc