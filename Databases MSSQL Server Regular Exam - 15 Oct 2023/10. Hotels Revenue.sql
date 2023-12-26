	--10
	select h.Name, sum(DATEDIFF(DAY,b.ArrivalDate,b.DepartureDate)*r.Price) as HotelRevenue from Bookings as b
	join Hotels as h on b.HotelId=h.Id
	join Rooms as r on b.RoomId= r.Id
	group by h.Name
	order by sum(DATEDIFF(DAY,b.ArrivalDate,b.DepartureDate)*r.Price) desc