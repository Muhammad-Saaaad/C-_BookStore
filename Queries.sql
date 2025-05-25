use e_commerce

create table Accounts (user_id varchar(40) primary key, email varchar(50), password varchar(50),
user_type varchar(10))

select * from Accounts

create table Products (user_id varchar(40), product_id int identity(1,1), product_name varchar(50), 
quantity int, price float, img_path varchar(max), 
constraint pk_upid primary key (user_id, product_id),
constraint fk_uid foreign key (user_id) references Accounts (user_id) )

select * from Products

alter table products add unique (product_id) 
-- now product id alone can be a unqiue column as well and can alone be use 
-- as a foreign key into another table


create table Purchase (purchase_id int,user_id varchar(40), product_id int, product_name varchar(50), 
quantity int, total_price float, purchase_date date,

constraint pk_purchase_upid primary key (purchase_id, user_id, product_id),
constraint fk_purchase_uid foreign key (user_id) references Accounts(user_id),
constraint fk_purchase_pid foreign key (product_id) references Products(product_id))

select * from Products
select * from Purchase

drop table Purchase
drop table Products

select product_id , product_name, quantity, price, img_path from Products


--insert into Accounts values ('user_id', 'email','password', 'type') 

select * from Accounts where user_id='ali123' and password='123'

