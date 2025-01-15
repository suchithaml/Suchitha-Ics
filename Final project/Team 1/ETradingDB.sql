Create database ETradingDB;

--Drop Database ETradingDB;

use ETradingDB;



USE [ETradingDB]
GO

INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password]
           ,[IsCustomer]
           ,[IsVendor]
           ,[IsAdmin])
     VALUES ('admin','admin123',0,0,1)




CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    IsCustomer BIT NOT NULL DEFAULT 0,
    IsVendor BIT NOT NULL DEFAULT 0,
    IsAdmin BIT NOT NULL DEFAULT 0,
    Status VARCHAR(20) DEFAULT 'Pending',
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY, -- needed change
    CustomerName VARCHAR(30) NOT NULL, 
    CustomerPhoneNo VARCHAR(10), 
    CustomerAddress VARCHAR(45), 
    CustomerEmailID VARCHAR(45), 
    VendorID INT DEFAULT -1 -- not required
);

CREATE TABLE Vendor (
    VendorID INT PRIMARY KEY,  -- needed change
    VendorName VARCHAR(30) NOT NULL, 
    VendorCompanyPhoneNo VARCHAR(10), 
    VendorCompanyAddress VARCHAR(45), 
    VendorCompanyEmailID VARCHAR(45), 
    VendorCompanyName VARCHAR(45) NOT NULL, 
    VendorIsActive BIT DEFAULT 1, 
    CustomerID INT DEFAULT -1-- not required
);

CREATE TABLE TopUp (
    TopID INT IDENTITY(1,1) PRIMARY KEY, 
    UserID INT, 
    AccNo BIGINT NOT NULL, 
    Balance DECIMAL(10, 2) DEFAULT 0.00
);

--drop table TopUp

CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY, 
    CategoryName VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY, 
    VendorID INT NOT NULL, 
    CategoryID INT NOT NULL, 
    ProductName VARCHAR(100) NOT NULL,
	Description TEXT NOT NULL, -- check 
    Price DECIMAL(10, 2) NOT NULL, 
    Stock BIGINT NOT NULL, 
	ImagePath NVARCHAR(MAX) NOT NULL, 
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Notifications (
    NotificationID INT IDENTITY(1,1) PRIMARY KEY, 
    ProductID INT NOT NULL, 
    OldPrice DECIMAL(10, 2) NOT NULL, 
    NewPrice DECIMAL(10, 2) NOT NULL, 
	--Content Varchar (30) NOT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY, 
    CustomerID INT NOT NULL, 
    VendorID INT NOT NULL, 
    OrderTotal DECIMAL(10, 2) NOT NULL, 
    OrderStatus VARCHAR(10) DEFAULT 'Completed', 
    OrderDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY, 
    OrderID INT NOT NULL, 
    ProductID INT NOT NULL, 
    Quantity INT NOT NULL, 
    TotalPrice DECIMAL(10, 2) NOT NULL
);

ALTER TABLE Users
ADD CONSTRAINT FK_User_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID);

ALTER TABLE Users
ADD CONSTRAINT FK_User_Vendor FOREIGN KEY (VendorID) REFERENCES Vendor(VendorID);

ALTER TABLE Customer
ADD CONSTRAINT FK_Customer_Vendor FOREIGN KEY (VendorID) REFERENCES Vendor(VendorID);

ALTER TABLE Vendor
ADD CONSTRAINT FK_Vendor_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID);

ALTER TABLE TopUp
ADD CONSTRAINT FK_TopUp_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID);

ALTER TABLE Products
ADD CONSTRAINT FK_Product_Vendor FOREIGN KEY (VendorID) REFERENCES Vendor(VendorID);

ALTER TABLE Products
ADD CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID);

ALTER TABLE Notifications
ADD CONSTRAINT FK_Notification_Product FOREIGN KEY (ProductID) REFERENCES Products(ProductID);

ALTER TABLE Orders
ADD CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerID) REFERENCES Users(UserID);

ALTER TABLE Orders
ADD CONSTRAINT FK_Order_Vendor FOREIGN KEY (VendorID) REFERENCES Users(UserID);

ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderDetail_Order FOREIGN KEY (OrderID) REFERENCES Orders(OrderID);

ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderDetail_Product FOREIGN KEY (ProductID) REFERENCES Products(ProductID);


------------------------------------------------------------------------------------------
--Stored Procedures
-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------
--Bhuvan & Suchitha Stored Procedures
-------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE GetTotalSales
AS
BEGIN
    SELECT COALESCE(SUM(OD.TotalPrice), 0) AS TotalSales FROM OrderDetails OD JOIN Orders O ON OD.OrderID = O.OrderID
    WHERE O.OrderStatus = 'Completed';
END;

--execute GetTotalSales;
-------------------------------------------------------------------------------------------\

-------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE GetTotalSalesByDate
    @Date Date
AS
BEGIN
    SELECT SUM(OD.TotalPrice) AS TotalSales, CAST(O.OrderDate AS DATE) AS 'Date' FROM OrderDetails OD 
    JOIN Orders O ON OD.OrderID = O.OrderID WHERE O.OrderStatus = 'Completed' AND CAST(O.OrderDate AS DATE) = @Date
    GROUP BY CAST(O.OrderDate AS DATE);
END;

--execute GetTotalSalesByDate '2025-01-14';
SELECT * FROM Orders;

-- OrderDetails Table
SELECT * FROM OrderDetails;

-------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE GetTopSellingProducts
AS
BEGIN
    SELECT TOP 5 P.ProductID,P.ProductName , SUM(OD.Quantity) AS TotalQuantitySold FROM OrderDetails OD 
	JOIN Products P ON OD.ProductID = P.ProductID JOIN Orders O ON OD.OrderID = O.OrderID
    WHERE O.OrderStatus = 'Completed' GROUP BY P.ProductID,P.ProductName  ORDER BY TotalQuantitySold DESC;
END;

--execute GetTopSellingProducts;
-------------------------------------------------------------------------------------------


-------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE GetTopSellingProductsByVendor
    @VendorID INT
AS
BEGIN
    SELECT TOP 5 P.ProductID,P.ProductName, SUM(OD.Quantity) AS TotalQuantitySold FROM OrderDetails OD 
    JOIN Products P ON OD.ProductID = P.ProductID JOIN Orders O ON OD.OrderID = O.OrderID
    WHERE O.OrderStatus = 'Completed' AND P.VendorID = @VendorID
    GROUP BY P.ProductID,P.ProductName ORDER BY TotalQuantitySold DESC;
END;

--execute GetTopSellingProductsByVendor 1;

-------------------------------------------------------------------------------------------
--Bharghav & Bhavani Stored Procedures
-------------------------------------------------------------------------------------------
CREATE or alter PROCEDURE GetProductDetails
    @ProductID INT
AS
BEGIN
    SELECT
        p.ProductID,
        p.ProductName,
        p.Description,
        p.Price,
        p.Stock,
        c.CategoryName,
        p.ImagePath
    FROM
        Products p
    JOIN
        Categories c ON p.CategoryID = c.CategoryID
    WHERE
        p.ProductID = @ProductID
END
 
 
-----------------------------------------------
 

CREATE or alter PROCEDURE GetNotificationsToday
AS
BEGIN
    SELECT
        n.NotificationID,
        p.ProductName,
        n.OldPrice,
        n.NewPrice,
        n.UpdatedAt
    FROM
        Notifications n
    JOIN
        Products p ON n.ProductID = p.ProductID
    WHERE
        CONVERT(DATE, n.UpdatedAt) = CONVERT(DATE, GETDATE())
    ORDER BY
        n.UpdatedAt DESC;
END;



-------------------------------------------------------------------------------------------
--Shree priya & Senbagam Stored Procedures
-------------------------------------------------------------------------------------------






---------------------------------------------------------------------------------
--DML
---------------------------------------------------------------------------------

-- Users Table
SELECT * FROM Users;

-- Customer Table
SELECT * FROM Customer;

-- Vendor Table
SELECT * FROM Vendor;

-- TopUp Table
SELECT * FROM TopUp;

-- Categories Table
SELECT * FROM Categories;

-- Products Table
SELECT * FROM Products;

-- Notifications Table
SELECT * FROM Notifications;

-- Orders Table
SELECT * FROM Orders;

-- OrderDetails Table
SELECT * FROM OrderDetails;






-- Users Table
TRUNCATE TABLE Users;
SELECT * FROM Users;

-- Customer Table
TRUNCATE TABLE Customer;

-- Vendor Table
TRUNCATE TABLE Vendor;

-- TopUp Table
TRUNCATE TABLE TopUp;

-- Categories Table
TRUNCATE TABLE Categories;

-- Products Table
TRUNCATE TABLE Products;

-- Notifications Table
TRUNCATE TABLE Notifications;

-- Orders Table
TRUNCATE TABLE Orders;

-- OrderDetails Table
TRUNCATE TABLE OrderDetails;

-- Users Table
DROP TABLE Users;

-- Customer Table
DROP TABLE Customer;

-- Vendor Table
DROP TABLE Vendor;

-- TopUp Table
DROP TABLE TopUp;

-- Categories Table
DROP TABLE Categories;

-- Products Table
DROP TABLE Products;

-- Notifications Table
DROP TABLE Notifications;

-- Orders Table
DROP TABLE Orders;

-- OrderDetails Table
DROP TABLE OrderDetails;







