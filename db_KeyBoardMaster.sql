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

create table statistics
(
id_statistics int primary key auto_increment,
id_user int,
count_word_min int,
accuracy_per int,
count_mistake int
);

insert into admin values
(1, "Admin", "Aris444741032", "cat");

select * from users;
select * from admin;
select * from statistics;