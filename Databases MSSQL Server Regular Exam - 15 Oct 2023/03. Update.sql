update Bookings
	set DepartureDate=DATEADD(DAY,1,DepartureDate)
	where ArrivalDate between '2023-12-1' and '2023-12-31'
	update Tourists
	set Email=null
	where Name like '%ma%'