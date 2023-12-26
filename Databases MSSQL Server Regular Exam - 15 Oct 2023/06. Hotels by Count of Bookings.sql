	select h.Id, h.Name from Hotels as h
	join HotelsRooms as hr on h.Id=hr.HotelId
	join Rooms as r on hr.RoomId=r.Id
	join Bookings as b on h.Id=b.HotelId
	where r.Type='VIP Apartment'
	group by h.Id,h.Name
	order by COUNT(b.Id) desc