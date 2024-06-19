CREATE DATABASE Sellers;
GO

USE Sellers;
GO

-- Create tables --

BEGIN TRANSACTION

CREATE TABLE ShopTypes (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE
);
GO

CREATE TABLE Districts (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    City NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Shops (
    ID INT IDENTITY(1,1) PRIMARY KEY,
	TypeID INT,
    DistrictID INT,
	Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    FOREIGN KEY (TypeID) REFERENCES ShopTypes(ID),
	FOREIGN KEY (DistrictID) REFERENCES Districts(ID)
);
GO

CREATE TABLE Vendors (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
	PhoneNumber NVARCHAR(20)
);
GO

CREATE TABLE VendorDistricts (
    VendorID INT NOT NULL,
    DistrictID INT NOT NULL,
    Role NVARCHAR(50) CHECK (Role IN ('primary', 'secondary')) NOT NULL DEFAULT 'secondary',
    PRIMARY KEY (VendorID, DistrictID),
    FOREIGN KEY (VendorID) REFERENCES Vendors(ID),
    FOREIGN KEY (DistrictID) REFERENCES Districts(ID)
);
GO

CREATE UNIQUE INDEX UX_PrimaryVendor ON VendorDistricts (DistrictID)
WHERE Role = 'primary';
GO

-- Insert test data --

INSERT INTO ShopTypes (Name)
VALUES ('Retail'), ('Supermarket'), ('Restaurant'), ('Cafe'), ('Pharmacy');
GO

INSERT INTO Districts (Name, City) 
VALUES 
('Downtown', 'Metropolis'),
('Market', 'Metropolis'),
('Old Town', 'Metropolis');
GO

INSERT INTO Shops (TypeID, DistrictID, Name, Address) 
VALUES 
(1, 1, 'Best Retail Shop', '123 Retail St'),
(2, 1, 'Super Supermarket', '456 Market St'),
(4, 2, 'Mega Coffe', '321 Revolution Bld'),
(3, 2, 'Italiano Pizza', '321 Revolution Bld');
GO

INSERT INTO Vendors (FirstName, LastName, Email, PhoneNumber) 
VALUES 
('John', 'Doe', 'john.doe@example.com', '123-456-7890'),
('Jane', 'Smith', 'jane.smith@example.com', '234-567-8901'),
('Robert', 'Johnson', 'robert.johnson@example.com', '345-678-9012'),
('Emily', 'Davis', 'emily.davis@example.com', '456-789-0123'),
('Michael', 'Brown', 'michael.brown@example.com', '567-890-1234'),
('Jessica', 'Williams', 'jessica.williams@example.com', '678-901-2345'),
('Angela', 'Simpson', 'angela.simpson@example.com', '435-234-3232');
GO

INSERT INTO VendorDistricts (VendorID, DistrictID, Role) 
VALUES 
(1, 1, 'primary'),
(1, 3, 'secondary'),
(2, 1, 'secondary'),
(3, 2, 'primary'), 
(4, 2, 'secondary'),
(5, 3, 'primary'),
(6, 3, 'secondary');
GO

COMMIT TRANSACTION