	create or alter proc usp_SearchByCountry(@country nvarchar(50))
	as 
	select t.Name,t.PhoneNumber,t.Email,Count(*) from Bookings as b
	join Tourists as t on b.TouristId=t.Id
	join Hotels as h on b.HotelId=h.Id
	join Destinations as d on h.DestinationId=d.Id
	join Countries as c on t.CountryId=c.Id
	where c.Name=@country
	group by  t.Name,t.PhoneNumber,t.Email