INSERT INTO Category(Code,Name) VALUES ('1-A','Kraska-1');
INSERT INTO Category(Code,Name) VALUES ('1-B','Kraska-2');
INSERT INTO WorkPlace (Name,Location) VALUES ('OCM-zavod', 'yangi ToshMi');
INSERT INTO WorkPlace (Name,Location) VALUES ('Bek-Baraka', 'Bek-Baraka Bozor');
INSERT INTO AccountUser (Name,Surname,PositionRole,PhoneNumber,WorkPlace) VALUES ('Jamshid', 'Davletov', 1,'998890121133',1);
INSERT INTO AccountUser (Name,Surname,PositionRole,PhoneNumber,WorkPlace) VALUES ('Davron', 'Iskandarov', 1,'9988922222233',2);
INSERT INTO Supplier (Name, SupplierInfo) VALUES ('OCM-zavod','Zavod');
INSERT INTO Supplier (Name, SupplierInfo) VALUES ('Hayyat','kistichkalar');
SELECT * FROM Supplier
INSERT INTO Product (Code,Name,Category,Price,Currency,MeasurementUnit,BoxedNumber,Supplier) VALUES ('12-AB','Oscar',1, 15.20,1,'Karobka',10,1);
INSERT INTO Product (Code,Name,Category,Price,Currency,MeasurementUnit,BoxedNumber,Supplier) VALUES ('22-AB','Kistichka',2,5000,2,'Shtuka',1,2);
INSERT INTO Client (Name,Address,Phone,PaymentAccount, BankDetails,City,MFO,INN,OKONX) VALUES ('Shamsiddin','Yunusobod','+99891234845','123-321-123','123-aded-123','Tashkent','awd-awd-awd','12313-12312-123123','awd-awdawdaw-awdaw');
INSERT INTO Client (Name,Address,Phone,PaymentAccount, BankDetails,City,MFO,INN,OKONX) VALUES ('Go`zal','Parkentskiy','+99893334845','123-321-123','333-aded-333','Tashkent','brt-brt-brt','32313-32312-323123','btd-awdawdaw-btbt');
INSERT INTO Expense (ExpenseDate,Cost,ExpenseCause,UserId) VALUES (getdate(),15000,'Obed',1);
INSERT INTO Expense (ExpenseDate,Cost,ExpenseCause,UserId) VALUES (getdate(),100000,'Arenda',2);
INSERT INTO ProductStock (ProductId,WorkPlaceId,Quantity) VALUES (1,1,10);
INSERT INTO ProductStock (ProductId,WorkPlaceId,Quantity) VALUES (2,2,20);

INSERT INTO ClientOrder (Saler,OrderDate,TransactinoId,PaymentByCash,PaymentByCard,PaymentByTransfer,IsDebt,ClientId,ClientRepresentitive, OverallPrice) VALUES (2,GETDATE(),'123-ABC-123',150 ,0,0, 1,2,'Jamshid',150);
INSERT INTO ClientOrder (Saler,OrderDate,TransactinoId,PaymentByCash,PaymentByCard,PaymentByTransfer,IsDebt,ClientId,ClientRepresentitive, OverallPrice) VALUES (1,GETDATE(),'123-ABC-123',0,10000,0, 1,1,'Aziz',10000);
SELECT * FROM Sale
INSERT INTO Sale (ProductId,ProductName,Quantity,ProductPrice,SalesPrice,OrderId) VALUES (1,'Oscar',10,20.00,200.00,1);
INSERT INTO Sale (ProductId,ProductName,Quantity,ProductPrice,SalesPrice,OrderId) VALUES (2,'Hayyat-kistichka',15,10000.00,150000.00,1);
INSERT INTO Sale (ProductId,ProductName,Quantity,ProductPrice,SalesPrice,OrderId) VALUES (1,'Oscar',10,20.00,200.00,2);
INSERT INTO ReturnedSale (SaleId,ReturnDate,DefectedQuantity,Quantity,ReturnedPrice, WorkplaceId,ProductId,ReturnedMoney) VALUES(3,GETDATE(),5,5,100.00,1,3,1000);
INSERT INTO ReturnedSale (SaleId,ReturnDate,DefectedQuantity,Quantity,ReturnedPrice, WorkplaceId,ProductId,ReturnedMoney) VALUES(4,GETDATE(),5,5,50000.00,1,2, 3000);
INSERT INTO InnerMovement (MoveDate,ProductId,Quantity,FromWorkPlaceId,ToWorkPlaceId) VALUES (GETDATE(),2,5,1,2);
INSERT INTO InnerMovement (MoveDate,ProductId,Quantity,FromWorkPlaceId,ToWorkPlaceId) VALUES (GETDATE(),1,7,2,1);
INSERT INTO DebtCover (CoverDate,PaymentByCash,PaymentByCard,PaymentByTransfer,OrderId) VALUES (GETDATE(),50,0,0,1);
INSERT INTO DebtCover (CoverDate,PaymentByCash,PaymentByCard,PaymentByTransfer,OrderId) VALUES (GETDATE(),0,0,5000,2);