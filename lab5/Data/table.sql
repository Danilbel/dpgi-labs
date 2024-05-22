create table units_of_measure
(
    id   serial primary key,
    name varchar(255) not null unique
);

create table products
(
    id              serial primary key,
    article         varchar(255) not null unique,
    name            varchar(255) not null,
    unit_of_measure_id integer references units_of_measure(id),
    quantity        decimal(10, 2),
    price           decimal(10, 2)
);

