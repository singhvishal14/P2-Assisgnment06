create database ProductInventoryDB
use ProductInventoryDB
create table Products(
ProductId int primary key,
ProductName nvarchar(50),
price decimal,
Quantity int,
MfDate Date,
ExpDate Date
)
insert into Products values(1,'Tide',766.8,9,'11/01/2022','12/02/2023')
insert into Products values(2,'Saffola',720,6,'11/01/2022','12/02/2023')
insert into Products values(3,'Oreo',60,6,'11/04/2022','12/12/2022')

select * from Products