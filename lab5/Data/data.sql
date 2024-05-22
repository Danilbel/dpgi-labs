insert into units_of_measure (id, name)
values (1, 'кг'),
       (2, 'шт'),
       (3, 'м');

insert into products (article, name, unit_of_measure_id, quantity, price)
values ('A101', 'Яблуко', 1, 20, 39),
       ('A102', 'Ручка', 2, 40, null),
       ('A103', 'Кабель', 3, 300, 23),
       ('A104', 'Чашка', null, 60, null);