	create function udf_RoomsWithTourists(@name nvarchar(50)) 
	returns int 
	as 
	begin
	declare @numoftur int =
	(
	select sum(AdultsCount+ChildrenCount) from Bookings as b 
	join Rooms as r on b.RoomId=r.Id
	where r.Type=@name
	
	)
	return @numoftur

	end