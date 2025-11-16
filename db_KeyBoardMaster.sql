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

insert into admin values
(1, "admin", "Aris444741032", "cat");

select * from users;
select * from admin;