/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

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