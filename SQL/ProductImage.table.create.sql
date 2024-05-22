create table ProductImage(
	ProductSN char(10) not null primary key,
	ProductImage varbinary(max),
	Creator char(7),
	CreatedDate datetime,
	Modifier char(7),
	ModifiedDate datetime
)