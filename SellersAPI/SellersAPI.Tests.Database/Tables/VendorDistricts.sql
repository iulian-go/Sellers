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