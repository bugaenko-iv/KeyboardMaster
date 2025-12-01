create database KeyboardMaster;
drop database KeyboardMaster;
use KeyboardMaster;

create table users
(
id_user int primary key auto_increment,
login varchar (100),
password varchar(100),
keyword varchar(100)
);

create table admin
(
id_user int primary key auto_increment,
login varchar (100),
password varchar(100),
keyword varchar(100)
);

insert into users values
(1, "IvanBug", "Aris444741032", "cat"),
(2, "EgorD", "Shanti12345", "dog"),
(3, "Ermol", "MakanTop123", "rap"),
(4, "ChichukBasket", "1Lakers2", "nba");

insert into admin values
(1, "Admin", "Aris444741032", "cat");

drop table users;
drop table admin;

select * from users;
select * from admin;