/*
insert into Product (ProductSN,ProductName,ProductPrice,Creator,CreatedDate) 
values
('0000000001','Ring',200,'9999999',GETDATE())

insert into ProductImage (ProductSN,ProductImage)
*/

select * from ProductImage where ProductSN = '0000000001'

select * from Product

select a.ProductSN,a.ProductName,b.ProductImage from Product a left join ProductImage b on a.ProductSN=b.ProductSN