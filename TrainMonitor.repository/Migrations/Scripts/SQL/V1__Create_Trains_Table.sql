CREATE TABLE trains
(
    id             varchar(50) NOT NULL  PRIMARY KEY,
    name   varchar(100),
    train_number varchar(100),
    delay_minutes int null ,
    next_stop varchar(100),
    last_updated timestamp
    
);