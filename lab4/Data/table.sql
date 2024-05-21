create table products
(
    id              serial primary key,
    article         varchar(255) not null unique,
    name            varchar(255) not null,
    unit_of_measure varchar(255),
    quantity        decimal(10, 2),
    price           decimal(10, 2)
);