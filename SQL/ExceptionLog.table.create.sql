create table ExceptionLog(
	ExceptionSN int identity not null primary key,
	ExceptionClass varchar(50),
	ExceptionMethod varchar(50),
	ExceptionReason nvarchar(max),
	ExceptionDate datetime
)