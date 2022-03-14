create database ChatRoom
go 

use ChatRoom
go

create table Chat
(
	Id nchar(36) primary key,
	Name nvarchar(250) not null,
	Type int not null,
	Created datetimeoffset
)
go

create table "User"
(
	Id nchar(36) primary key,
	Username nvarchar(250) not null constraint uk_user unique,
	Name nvarchar(250) not null,
	Email nvarchar(250) not null constraint uk_user_email unique,
	Password nvarchar(max) not null,
	Created datetimeoffset
)
go

create table UserChat
(
	Id nchar(36) primary key,
	UserId nchar(36),
	ChatId nchar(36),
	Created datetimeoffset
)
go

alter table UserChat add constraint uk_user_chat unique(UserId, ChatId)
go

-- Default chats
insert Chat (Id, Name, Type, Created) values 
(N'12e7a17c-f43d-4dab-82fd-88de5b5f376b', N'Single', 0, CAST(N'2022-03-13T13:34:45.8824581+00:00' AS DateTimeOffset)),
(N'380a64c5-650b-4d0a-a256-0b73cab6b039', N'Single', 0, CAST(N'2022-03-13T13:35:19.6663502+00:00' AS DateTimeOffset)),
(N'59b30f65-7eec-4d3a-a658-0f7e226f24dc', N'Single', 0, CAST(N'2022-03-13T22:10:51.1914550+00:00' AS DateTimeOffset)),
(N'7018b696-a508-475a-b767-338bf52b540f', N'Web ChatRoom', 1, CAST(N'2022-03-13T13:35:26.4386156+00:00' AS DateTimeOffset)),
(N'581c0e7a-c433-4d99-b6ed-2608c7c3fd35', N'Greatest ChatRoom Ever', 1, CAST(N'2022-03-13T13:35:37.5352744+00:00' AS DateTimeOffset))
go

-- Default users
insert "User" (Id, Username, Name, Email, Password, Created) values 
(N'581c0e7a-c433-4d99-b6ed-2608c7c3fd35', N'jopimentel', N'Juan Osiris Pimentel', N'osiris2895@gmail.com', N'MTIzNA==', CAST(N'2020-03-13T07:01:00.0000000-04:00' AS DateTimeOffset)),
(N'7f7b7dd7-f4ed-48f5-8ea9-8a86c96243a2', N'shawn', N'Shawn Mendes', N'shawn@gmail.com', N'c2hhd25AMQ==', CAST(N'2020-03-13T07:01:00.0000000-04:00' AS DateTimeOffset)),
(N'99ee487f-b7ba-4190-86be-412121ed3dde', N'themovie', N'John Wick', N'john@gmail.com', N'am9obkB3aWNr', CAST(N'2020-03-13T07:01:00.0000000-04:00' AS DateTimeOffset)),
(N'cdc2a2de-d1c7-4bf8-8b43-9f95b6e3f293', N'badbunny', N'Bad Bunny', N'bad@gmail.com', N'dGVzdEAx', CAST(N'2020-03-13T07:01:00.0000000-04:00' AS DateTimeOffset))
go

-- Add default users to Web ChatRoom
insert into UserChat (Id, UserId, ChatId, Created) values 
(lower(newid()), '581c0e7a-c433-4d99-b6ed-2608c7c3fd35', '7018b696-a508-475a-b767-338bf52b540f', sysdatetimeoffset()),
(lower(newid()), '7f7b7dd7-f4ed-48f5-8ea9-8a86c96243a2', '7018b696-a508-475a-b767-338bf52b540f', sysdatetimeoffset()),
(lower(newid()), '99ee487f-b7ba-4190-86be-412121ed3dde', '7018b696-a508-475a-b767-338bf52b540f', sysdatetimeoffset()),
(lower(newid()), 'cdc2a2de-d1c7-4bf8-8b43-9f95b6e3f293', '7018b696-a508-475a-b767-338bf52b540f', sysdatetimeoffset())
go

-- Add users to Greatest ChatRoom Ever
insert into UserChat (Id, UserId, ChatId, Created) values 
(lower(newid()), '581c0e7a-c433-4d99-b6ed-2608c7c3fd35', '581c0e7a-c433-4d99-b6ed-2608c7c3fd35', sysdatetimeoffset()),
(lower(newid()), '7f7b7dd7-f4ed-48f5-8ea9-8a86c96243a2', '581c0e7a-c433-4d99-b6ed-2608c7c3fd35', sysdatetimeoffset()),
(lower(newid()), '99ee487f-b7ba-4190-86be-412121ed3dde', '581c0e7a-c433-4d99-b6ed-2608c7c3fd35', sysdatetimeoffset())
go

-- Add chat between users
insert into UserChat (Id, UserId, ChatId, Created) values 
(lower(newid()), '581c0e7a-c433-4d99-b6ed-2608c7c3fd35', '12e7a17c-f43d-4dab-82fd-88de5b5f376b', sysdatetimeoffset()),
(lower(newid()), '581c0e7a-c433-4d99-b6ed-2608c7c3fd35', '380a64c5-650b-4d0a-a256-0b73cab6b039', sysdatetimeoffset()),
(lower(newid()), '581c0e7a-c433-4d99-b6ed-2608c7c3fd35', '59b30f65-7eec-4d3a-a658-0f7e226f24dc', sysdatetimeoffset()),
(lower(newid()), '7f7b7dd7-f4ed-48f5-8ea9-8a86c96243a2', '12e7a17c-f43d-4dab-82fd-88de5b5f376b', sysdatetimeoffset()),
(lower(newid()), '99ee487f-b7ba-4190-86be-412121ed3dde', '380a64c5-650b-4d0a-a256-0b73cab6b039', sysdatetimeoffset()),
(lower(newid()), 'cdc2a2de-d1c7-4bf8-8b43-9f95b6e3f293', '59b30f65-7eec-4d3a-a658-0f7e226f24dc', sysdatetimeoffset())
go
