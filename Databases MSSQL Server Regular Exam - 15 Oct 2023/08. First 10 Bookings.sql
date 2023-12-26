	select top 10 h.Name,d.Name,c.Name from  Bookings as b 
	join Hotels as h on b.HotelId=h.Id
	join Destinations as d on h.DestinationId=d.Id
	join Countries as c on d.CountryId=c.Id
	
	where b.ArrivalDate<'2023-12-31' and h.Id%2=1
	order by c.Name,b.ArrivalDate