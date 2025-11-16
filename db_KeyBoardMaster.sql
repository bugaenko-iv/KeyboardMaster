create database KeyboardMaster;
drop database KeyboardMaster;
use KeyboardMaster;

create table users
(
id_user int primary key auto_increment,
login varchar (100),
password varchar(100)
);