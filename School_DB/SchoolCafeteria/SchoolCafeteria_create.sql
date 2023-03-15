-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2023-03-14 14:11:13.243

CREATE DATABASE SchoolCafeteria
GO

USE SchoolCafeteria
GO

-- tables
-- Table: Customers
CREATE TABLE Customers (
    CustomerId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    Email nvarchar(80)  NOT NULL,
    Phone nvarchar(20)  NOT NULL,
    CONSTRAINT Customers_pk PRIMARY KEY  (CustomerId)
);

-- Table: Employees
CREATE TABLE Employees (
    EmployeeId uniqueidentifier  NOT NULL,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    Position nvarchar(30)  NOT NULL,
    Salary decimal(6,2)  NOT NULL,
    CONSTRAINT Employees_pk PRIMARY KEY  (EmployeeId)
);

-- Table: MenuItems
CREATE TABLE MenuItems (
    MenuItemId uniqueidentifier  NOT NULL,
    Name nvarchar(50)  NOT NULL,
    Description nvarchar(200)  NOT NULL,
    Price decimal(5,2)  NOT NULL,
    CONSTRAINT MenuItems_pk PRIMARY KEY  (MenuItemId)
);

-- Table: Orders
CREATE TABLE Orders (
    OrderId uniqueidentifier  NOT NULL,
    MenuItemId uniqueidentifier  NOT NULL,
    OrderTime date  NOT NULL,
    Quantity int  NOT NULL,
    CONSTRAINT Orders_pk PRIMARY KEY  (OrderId)
);

-- Table: OrdersCustomers
CREATE TABLE OrdersCustomers (
    OrderId uniqueidentifier  NOT NULL,
    CustomerId uniqueidentifier  NOT NULL,
    CONSTRAINT OrdersCustomers_pk PRIMARY KEY  (OrderId,CustomerId)
);

-- Table: OrdersEmployees
CREATE TABLE OrdersEmployees (
    OrderId uniqueidentifier  NOT NULL,
    EmployeeId uniqueidentifier  NOT NULL,
    CONSTRAINT OrdersEmployees_pk PRIMARY KEY  (OrderId,EmployeeId)
);

-- Table: Payments
CREATE TABLE Payments (
    PaymentId uniqueidentifier  NOT NULL,
    OrderId uniqueidentifier  NOT NULL,
    PaymentTime date  NOT NULL,
    Amount decimal(8,2)  NOT NULL,
    CONSTRAINT Payments_pk PRIMARY KEY  (PaymentId)
);

-- foreign keys
-- Reference: MenuItems_Orders (table: Orders)
ALTER TABLE Orders ADD CONSTRAINT MenuItems_Orders
    FOREIGN KEY (MenuItemId)
    REFERENCES MenuItems (MenuItemId);

-- Reference: OrdersCustomers_Customers (table: OrdersCustomers)
ALTER TABLE OrdersCustomers ADD CONSTRAINT OrdersCustomers_Customers
    FOREIGN KEY (CustomerId)
    REFERENCES Customers (CustomerId);

-- Reference: OrdersCustomers_Orders (table: OrdersCustomers)
ALTER TABLE OrdersCustomers ADD CONSTRAINT OrdersCustomers_Orders
    FOREIGN KEY (OrderId)
    REFERENCES Orders (OrderId);

-- Reference: OrdersEmployees_Employees (table: OrdersEmployees)
ALTER TABLE OrdersEmployees ADD CONSTRAINT OrdersEmployees_Employees
    FOREIGN KEY (EmployeeId)
    REFERENCES Employees (EmployeeId);

-- Reference: OrdersEmployees_Orders (table: OrdersEmployees)
ALTER TABLE OrdersEmployees ADD CONSTRAINT OrdersEmployees_Orders
    FOREIGN KEY (OrderId)
    REFERENCES Orders (OrderId);

-- Reference: Payments_Orders (table: Payments)
ALTER TABLE Payments ADD CONSTRAINT Payments_Orders
    FOREIGN KEY (OrderId)
    REFERENCES Orders (OrderId);

-- End of file.

