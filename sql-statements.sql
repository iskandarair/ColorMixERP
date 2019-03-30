IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'Category')
CREATE  TABLE  Category (
Id int IDENTITY(1,1),
Code nvarchar(30) NOT NULL,
Name nvarchar(255) NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
Primary Key (Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'WorkPlace')
CREATE TABLE WorkPlace (
Id int IDENTITY(1,1),
Name nvarchar(255) NOT NULL,
Location nvarchar(255) NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
Primary Key (Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'AccountUser')
CREATE TABLE AccountUser (
Id int IDENTITY(1,1),
Name nvarchar(255) NOT NULL,
Surname nvarchar(255),
PositionRole int NOT NULL,
PhoneNumber nvarchar(255),
WorkPlace int NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
Password nvarchar(255) NOT NULL default '_M?;Z?e??''?????',
isSunnat bit NOT NULL default 0,
PRIMARY KEY (Id),
CONSTRAINT UserWorkPlace FOREIGN KEY (WorkPlace) REFERENCES WorkPlace(Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'Supplier')
CREATE TABLE Supplier (
Id int IDENTITY(1,1),
Name nvarchar(255) NOT NULL,
SupplierInfo nvarchar(255) NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
IsLocal bit NOT NULL DEFAULT(0),
PRIMARY KEY (Id),
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'Product')
CREATE TABLE Product (
Id int IDENTITY(1,1),
Code nvarchar(30) NOT NULL,
Name nvarchar(255) NOT NULL,
Category int NOT NULL,
Price decimal(19,2) NOT NULL,
Currency int NOT NULL,
MeasurementUnit nvarchar(30) NOT NULL,
BoxedNumber decimal(19,2) NOT NULL,
Supplier INT NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
PRIMARY KEY (Id),
CONSTRAINT ProductSupplier FOREIGN KEY (Supplier) REFERENCES Supplier(Id),
CONSTRAINT ProductCategory FOREIGN KEY (Category) REFERENCES Category(Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'Client')
CREATE TABLE Client (
Id int IDENTITY(1,1),
Name nvarchar(255) NOT NULL,
Address nvarchar(255) NOT NULL,
Phone nvarchar(255) NOT NULL,
PaymentAccount nvarchar(255) NOT NULL,
BankDetails nvarchar(255) NOT NULL,
City nvarchar(255) NOT NULL,
MFO nvarchar(255) NOT NULL,
INN nvarchar(255) NOT NULL,
OKONX nvarchar(255) NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
NickName nvarchar(255),
 DebtorCreditor decimal(19,2) NOT NULL default(0),
PRIMARY KEY (Id),
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'Expense')
CREATE TABLE Expense (
Id int IDENTITY(1,1),
ExpenseDate DateTime  default CURRENT_TIMESTAMP,
Cost decimal(19,2) NOT NULL,
ExpenseCause nvarchar(255) NOT NULL,
UserId int NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
PRIMARY KEY (Id),
CONSTRAINT ExpenseUser FOREIGN KEY (UserId) REFERENCES AccountUser(Id)
); 

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'ProductStock')
CREATE TABLE ProductStock (
Id int IDENTITY(1,1),
ProductId int NOT NULL,
WorkPlaceId int NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
Quantity decimal(19,2) NOT NULL default 0
PRIMARY KEY (Id),
CONSTRAINT ProductStockProductId FOREIGN KEY (ProductId) REFERENCES Product(Id),
CONSTRAINT ProductStockWorkPlaceId FOREIGN KEY (WorkPlaceId) REFERENCES WorkPlace(Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'ClientOrder')
CREATE TABLE ClientOrder (
Id int IDENTITY(1,1),
Saler int NOT NULL,
OrderDate DateTime default CURRENT_TIMESTAMP,
TransactinoId nvarchar(30), -- NOT NULL ??
PaymentByCash decimal(19,2) NOT NULL,
PaymentByCard decimal(19,2) NOT NULL,
PaymentByTransfer decimal(19,2) NOT NULL,
IsDebt bit,
ClientId int,
ClientRepresentitive nvarchar(255),
OverallPrice decimal(19,2) NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
PRIMARY KEY (Id),
CONSTRAINT Saler FOREIGN KEY (Saler) REFERENCES AccountUser(Id),
CONSTRAINT ClientId FOREIGN KEY (ClientId) REFERENCES Client(Id)
); 

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'Sale')
CREATE TABLE Sale (
Id int IDENTITY(1,1),
ProductId int,
ProductName nvarchar(255),
Quantity decimal(19,2) NOT NULL,
ProductPrice decimal(19,2) NOT NULL,
SalesPrice decimal(19,2) NOT NULL,
OrderId int NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
CurrencyRate decimal(19,2),
PRIMARY KEY (Id),
CONSTRAINT SaleProductId FOREIGN KEY (ProductID) REFERENCES Product(Id),
CONSTRAINT OrderId FOREIGN KEY (OrderId) REFERENCES ClientOrder(Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'ReturnedSale')
CREATE TABLE ReturnedSale (
Id int IDENTITY(1,1),
SaleId int NOT NULL,
ReturnDate Datetime default CURRENT_TIMESTAMP,
Cause nvarchar(255),
DefectedQuantity decimal(19,2) NOT NULL,
Quantity decimal(19,2) NOT NULL,
ReturnedPrice decimal(19,2) NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
PRIMARY KEY (Id),
CONSTRAINT SaleId FOREIGN KEY (SaleId) REFERENCES Sale(Id),
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'InnerMovement')
CREATE TABLE InnerMovement (
Id int IDENTITY(1,1),
MoveDate DateTime  default CURRENT_TIMESTAMP,
ProductId int NOT NULL,
Quantity decimal(19,2) NOT NULL,
FromWorkPlaceId int NOT NULL,
ToWorkPlaceId int NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
GroupID int,
CreatedDate datetime,
TotalPrice  decimal(19,2),
PRIMARY KEY (Id),
CONSTRAINT ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id),
CONSTRAINT FromWorkPlace FOREIGN KEY (FromWorkPlaceId) REFERENCES WorkPlace(Id),
CONSTRAINT ToWorkPlace FOREIGN KEY (ToWorkPlaceId) REFERENCES WorkPlace(Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'DebtCover')
CREATE TABLE DebtCover (
Id int IDENTITY(1,1),
CoverDate DateTime  default CURRENT_TIMESTAMP,
PaymentByCash decimal(19,2) NOT NULL,
PaymentByCard decimal(19,2) NOT NULL,
PaymentByTransfer decimal(19,2) NOT NULL,
OrderId int NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
PRIMARY KEY (Id),
CONSTRAINT DebtCoverOrderId FOREIGN KEY (OrderId) REFERENCES ClientOrder(Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'Income')
CREATE TABLE Income (
Id int IDENTITY(1,1),
UserId int not null,
FromWorkPlace int not null,
ToWorkPlace int not null,
CreatedDate datetime default getDate(),
UpdatedDate datetime default getDate(),
IsProductStock bit not null default(0),
CONSTRAINT UserIdReference FOREIGN KEY (UserId) REFERENCES AccountUser(Id),
CONSTRAINT FromWorkPlaceReference FOREIGN KEY (FromWorkPlace) REFERENCES WorkPlace(Id),
CONSTRAINT ToWorkPlaceReference FOREIGN KEY (ToWorkPlace) REFERENCES WorkPlace(Id),
PRIMARY KEY (Id),
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'IncomeProduct')
CREATE TABLE IncomeProduct (
Id int IDENTITY(1,1),
IncomeId int not null,
ProductId int not null,
Quantity decimal(19,2) NOT NULL default 0,
CreatedDate datetime default getDate(),
UpdatedDate datetime default getDate(),
CONSTRAINT IncomeIdReference FOREIGN KEY (IncomeId) REFERENCES Income(Id),
CONSTRAINT ProductIdReference FOREIGN KEY (ProductId) REFERENCES Product(Id),
PRIMARY KEY (Id),
);

IF NOT EXISTS (SELECT [name] FROM sys.tables WHERE [name] = 'DebtorCreditor')
CREATE TABLE DebtorCreditor (
Id int IDENTITY(1,1),
ClientId int not null,
Amount decimal(19,2) NOT NULL default 0,
IsDebtor  bit NOT NULL DEFAULT(0),
CreatedDate datetime default getDate(),
UpdatedDate datetime default getDate(),
CONSTRAINT ClientIdREference FOREIGN KEY (ClientId) REFERENCES Client(id)
);

IF NOT EXISTS (SELECT [name] FROM sys.tables WHERE [name] = 'DailyBalance')
CREATE TABLE DailyBalance (
Id int IDENTITY(1,1),
ProductId int not null,
BalanceDate datetime default getDate(),
Quantity decimal(19,2) NOT NULL default 0,
 WorkPlaceId int not null,
CONSTRAINT WorkPlaceIdDailyBalance FOREIGN KEY (WorkPlaceId) REFERENCES WorkPlace(Id),
CONSTRAINT ProductIdRef FOREIGN KEY (ProductId) REFERENCES Product(Id)
);

IF NOT EXISTS  (  SELECT [name]  FROM sys.tables WHERE [name] = 'CompanyInfo')
CREATE TABLE CompanyInfo (
Id int IDENTITY(1,1),
LegalName nvarchar(255) NOT NULL,
Address nvarchar(255) NOT NULL,
Phone nvarchar(255) NOT NULL,
PaymentAccount nvarchar(255) NOT NULL,
BankDetails nvarchar(255) NOT NULL,
City nvarchar(255) NOT NULL,
MFO nvarchar(255) NOT NULL,
INN nvarchar(255) NOT NULL,
OKONX nvarchar(255) NOT NULL,
Director nvarchar(255) NOT NULL,
Accountant nvarchar(255) NOT NULL,
IsDeleted bit NOT NULL DEFAULT(0),
DeletedDate datetime default getDAte(),
UpdatedDate datetime default getDAte(),
PRIMARY KEY (Id),
);