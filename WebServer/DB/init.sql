create database HamsterHouse;
use HamsterHouse;

create table users(
id_user bigint primary key auto_increment not null,
mail varchar(100),
password varchar(255)
);

create table user_details(
 user_id bigint not null,
 nickname varchar(50),
 promocode varchar(50),
 balance decimal,
 birth_date date,
 foreign key(user_id) references users(id_user)
 on delete cascade on update cascade
);

create table games(
id_game bigint not null auto_increment primary key,
name varchar(50) not null,
isMultiplayer bool not null,
count_of_playes int not null
);


