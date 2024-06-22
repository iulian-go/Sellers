namespace SellersAPI.Tests
{
    using SellersAPI.Entities;
    using System.Collections.Generic;

    internal class ExpectedData
    {
        public static readonly List<ShopType> ShopTypes =
        [
            new ShopType { Id = 1, Name = "Retail"},
            new ShopType { Id = 2, Name = "Supermarket"},
            new ShopType { Id = 3, Name = "Restaurant"},
            new ShopType { Id = 4, Name = "Cafe"}
        ];

        public static readonly List<District> Districts =
        [
            new District { Id = 1, Name = "Downtown", City = "Metropolis" },
            new District { Id = 2, Name = "Market", City = "Metropolis" },
            new District { Id = 3, Name = "Old Town", City = "Metropolis" }
        ];

        public static readonly List<Shop> Shops =
        [
            new Shop { Id = 1, TypeId = 1, DistrictId = 1, Name = "Best Retail Shop", Address = "123 Retail St" },
            new Shop { Id = 2, TypeId = 2, DistrictId = 1, Name = "Super Supermarket", Address = "456 Market St" },
            new Shop { Id = 3, TypeId = 4, DistrictId = 2, Name = "Mega Coffe", Address = "321 Revolution Bld" },
            new Shop { Id = 4, TypeId = 3, DistrictId = 2, Name = "Italiano Pizza", Address = "321 Revolution Bld" }
        ];

        public static readonly List<Vendor> Vendors =
        [
            new Vendor { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123-456-7890" },
            new Vendor { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "234-567-8901" },
            new Vendor { Id = 3, FirstName = "Robert", LastName = "Johnson", Email = "robert.johnson@example.com", PhoneNumber = "345-678-9012" },
            new Vendor { Id = 4, FirstName = "Emily", LastName = "Davis", Email = "emily.davis@example.com", PhoneNumber = "456-789-0123" },
            new Vendor { Id = 5, FirstName = "Michael", LastName = "Brown", Email = "michael.brown@example.com", PhoneNumber = "567-890-1234" },
            new Vendor { Id = 6, FirstName = "Jessica", LastName = "Williams", Email = "jessica.williams@example.com", PhoneNumber = "678-901-2345" },
            new Vendor { Id = 7, FirstName = "Angela", LastName = "Simpson", Email = "angela.simpson@example.com", PhoneNumber = "435-234-3232" }
        ];

        public static readonly List<VendorDistrict> VendorDistricts =
        [
            new VendorDistrict { VendorId = 1, DistrictId = 1, Role = "primary" },
            new VendorDistrict { VendorId = 1, DistrictId = 3, Role = "secondary" },
            new VendorDistrict { VendorId = 2, DistrictId = 1, Role = "secondary" },
            new VendorDistrict { VendorId = 3, DistrictId = 2, Role = "primary" },
            new VendorDistrict { VendorId = 4, DistrictId = 2, Role = "secondary" },
            new VendorDistrict { VendorId = 5, DistrictId = 3, Role = "primary" },
            new VendorDistrict { VendorId = 6, DistrictId = 3, Role = "secondary" }
        ];
    }
}
