Create table  Countries(
Id int primary key identity,
Name nvarchar(50) not null

)
Create table Destinations (
Id int primary key identity,
Name varchar(50) not null,
CountryId int references Countries(Id) not null

)
Create table Rooms (
Id int primary key identity,
Type varchar(40) not null,
Price decimal(18,2) not null,
BedCount int check(BedCount between 1 and 10) not null,

)
Create table  Hotels(
Id int primary key identity,
Name varchar(50) not null,
DestinationId int references Destinations(Id) not null

)
Create table  Tourists(
Id int primary key identity,
Name nvarchar(80) not null,
PhoneNumber varchar(20) not null,
Email varchar(80),
CountryId int references Countries(Id) not null

)
Create table  Bookings(
Id int primary key identity,
ArrivalDate DateTime2 not null,
DepartureDate DateTime2 not null,
AdultsCount int check(AdultsCount between 1 and 10)  not null,
ChildrenCount int check(ChildrenCount between 0 and 9) not null,
TouristId int references Tourists(Id) not null,
HotelId int references Hotels(Id) not null,
RoomId int references Rooms(Id) not null

)
Create table HotelsRooms (
HotelId int references Hotels(Id),
RoomId int references Rooms(Id),
primary key(HotelId,RoomId)
)