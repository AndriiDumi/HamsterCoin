drop database if exists HamsterHouse;
create database HamsterHouse;
use HamsterHouse;

create table users(
    id bigint auto_increment primary key,
    mail varchar(100),
    password varchar(255)
);

create table user_details(
    id bigint primary key,
    user_id bigint foreign key
    nickname varchar(50),
    promocode varchar(50),
    balance decimal,
    birth_date date,
    
    foreign key(user_id) references users(id)
    on delete no action on update cascade
);

create table games(
    game_id bigint auto_increment primary key,
    name varchar(50) not null,
    isMultiplayer bool not null,
    count_players int not null
);

create table dep_history (
    id int auto_increment primary key,
    user_id bigint, 
    sum_dep decimal not null,
    date_dep datetime not null,

    foreign key(user_id) 
    references users(id)
        on delete no action
        on update cascade
);

create table withdraw_history (
    id int auto_increment primary key,
    user_id bigint not null, 
    sum_withdraw decimal not null,
    date_withdraw datetime not null,

    foreign key(user_id) 
    references users(id)
        on delete no action
        on update cascade
);

create table cards (
    id bigint auto_increment primary key,
    number varchar(12) not null,
    date varchar(4) not null,
    cvv varchar(3) not null
);

create table user_cards(
    user_id bigint not null,
    card_id bigint not null,

    foreign key(user_id) 
    references users(id)
        on delete no action
        on update cascade,
    
    foreign key(card_id) 
    references cards(id)
        on delete no action
        on update cascade
        
);
