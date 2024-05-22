create table Product(
	ProductSN char(10) not null primary key,
	ProductName nvarchar(10),
	ProductPrice int,
	ProductDescription nvarchar(50),
	Creator char(7),
	CreatedDate datetime,
	Modifier char(7),
	ModifiedDate datetime
)