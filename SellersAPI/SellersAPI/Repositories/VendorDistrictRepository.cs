namespace SellersAPI.Repositories
{
    using Dapper;
    using SellersAPI.Context;
    using SellersAPI.DTOs;
    using SellersAPI.Entities;
    using SellersAPI.Interfaces;
    using System.Threading.Tasks;

    public class VendorDistrictRepository : IVendorDistrictRepository
    {
        private readonly DapperContext context;

        public VendorDistrictRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<int> AssignVendorAsync(AssignmentDTO assignment)
        {
            var query = @"INSERT INTO VendorDistricts (VendorID, DistrictID)
                          VALUES (@vendorId, @districtId)";

            return await this.ExecuteQuery(query, assignment);
        }

        public async Task<int> ChangePrimaryAsync(AssignmentDTO assignment)
        {
            var query = @"BEGIN TRANSACTION

                          UPDATE VendorDistricts
                          SET Role = 'secondary'
                          WHERE DistrictID = @districtId AND Role = 'primary'

                          UPDATE VendorDistricts
                          SET Role = 'primary'
                          WHERE VendorID = @vendorId AND DistrictID = @districtId

                          COMMIT TRANSACTION";

            return await this.ExecuteQuery(query, assignment);
        }

        public async Task<int> RemoveVenorAsync(AssignmentDTO assignment)
        {
            var query = @"DELETE FROM VendorDistricts
                          WHERE VendorID = @vendorId AND DistrictID = @districtId";

            return await this.ExecuteQuery(query, assignment);
        }

        public async Task<VendorDistrict> GetAssociationAsync(AssignmentDTO assignment)
        {
            var query = "SELECT * FROM VendorDistricts WHERE VendorID = @vendorId AND DistrictID = @districtId";

            using (var connection = this.context.CreateConnection())
            {
                var association = await connection.QuerySingleOrDefaultAsync<VendorDistrict>(query, new { assignment.VendorId, assignment.DistrictId });
                return association;
            }
        }

        private async Task<int> ExecuteQuery(string query, AssignmentDTO assignment)
        {
            var parameters = new DynamicParameters();
            parameters.Add("vendorId", assignment.VendorId);
            parameters.Add("districtId", assignment.DistrictId);

            using (var connection = this.context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
