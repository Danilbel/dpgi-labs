create table users (
    id serial primary key,
    username varchar(50) not null,
    password varchar(250) not null
);

create table history (
    id serial primary key,
    user_id integer not null,
    date timestamp not null default now(),
    operation varchar(50) not null,
    input_text text not null,
    key bigint not null,
    output_text text not null
);