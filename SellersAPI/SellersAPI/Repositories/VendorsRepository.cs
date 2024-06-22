namespace SellersAPI.Repositories
{
    using Dapper;
    using SellersAPI.Entities;
    using SellersAPI.Interfaces;

    public class VendorsRepository : IVendorsRepository
    {
        private readonly IDbContext context;

        public VendorsRepository(IDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Vendor>> GetAllAsync()
        {
            var query = @"SELECT v.*, DistrictID, Role 
                          FROM Vendors v 
                          LEFT JOIN VendorDistricts vd ON vd.VendorID = v.ID
                          ORDER BY vd.Role";

            using (var connection = this.context.CreateConnection())
            {
                var vendorDictionary = new Dictionary<int, Vendor>();

                var result = await connection.QueryAsync<Vendor, VendorDistrict, Vendor>(query,
                    (vendor, vendorDist) =>
                    {
                        if (!vendorDictionary.TryGetValue(vendor.Id, out var current))
                        {
                            current = vendor;
                            current.Districts = new List<District>();
                            vendorDictionary.Add(current.Id, current);
                        }

                        if (vendorDist != null)
                        {
                            current.Districts.Add(new District { Id = vendorDist.DistrictId });
                            current.Role ??= vendorDist.Role;
                        }

                        return current;
                    },
                    splitOn: "DistrictID");

                return result.Distinct().ToList();
            }
        }

        public async Task<IEnumerable<Vendor>> GetAllByDistrictAsync(int districtId)
        {
            var query = @"SELECT v.*, vd.DistrictID, vd.Role 
                          FROM Vendors v 
                          JOIN VendorDistricts vd ON vd.VendorID = v.ID
                          JOIN Districts d ON d.ID = vd.DistrictID
                          WHERE d.ID = @districtId
                          ORDER BY vd.Role";

            using (var connection = this.context.CreateConnection())
            {
                var vendors = await connection.QueryAsync<Vendor, VendorDistrict, Vendor>(query,
                    (vendor, vendorDist) =>
                    {
                        vendor.Role = vendorDist.Role;
                        return vendor;
                    },
                    splitOn: "DistrictID", param: new { districtId });

                return vendors.ToList();
            }
        }

        public async Task<Vendor> GetByIdAsync(int id)
        {
            //var query = "SELECT * FROM Vendors WHERE Id = @Id";
            var query = @"SELECT v.*, vd.Role
                          FROM Vendors v
                          JOIN VendorDistricts vd ON vd.VendorID = v.ID
                          WHERE v.ID = @Id
                          ORDER BY vd.Role";

            using (var connection = this.context.CreateConnection())
            {
                var vendor = await connection.QueryFirstOrDefaultAsync<Vendor>(query, new { id });
                return vendor;
            }
        }
    }
}
