namespace SellersAPI.Tests.Repositories
{
    using Dapper;
    using SellersAPI.DTOs;
    using SellersAPI.Entities;
    using SellersAPI.Repositories;
    using System.Threading.Tasks;

    public class VendorDistrictRepositoryTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture fixture;
        private readonly VendorDistrictRepository repository;

        public VendorDistrictRepositoryTests(TestFixture fixture)
        {
            this.fixture = fixture;
            this.repository = new VendorDistrictRepository(fixture.Context);
        }

        [Fact]
        public async Task AssignAndRemoveVendorAsync()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            // Assign vendor
            var rowsAffected = await this.repository.AssignVendorAsync(assignment);

            Assert.Equal(1, rowsAffected);
            await this.EnsureVendorDistrictResult(7, 1, "secondary");

            // Remove vendor - Cleanup
            var affectedRows = await this.repository.RemoveVenorAsync(assignment);
            Assert.Equal(1, affectedRows);
        }

        [Fact]
        public async Task ChangePrimaryAsync()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 2, DistrictId = 1 };
            // Change vendor to primary
            var rowsAffected = await this.repository.ChangePrimaryAsync(assignment);

            Assert.Equal(2, rowsAffected);
            await this.EnsureVendorDistrictResult(2, 1, "primary");
            await this.EnsureVendorDistrictResult(1, 1, "secondary");

            AssignmentDTO cleanup = new AssignmentDTO { VendorId = 1, DistrictId = 1 };
            // Change back - Cleanup
            var affectedRows = await this.repository.ChangePrimaryAsync(cleanup);
            Assert.Equal(2, affectedRows);
            await this.EnsureVendorDistrictResult(1, 1, "primary");
            await this.EnsureVendorDistrictResult(2, 1, "secondary");
        }

        [Fact]
        public async Task GetAssociationAsync()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 1, DistrictId = 1 };
            var association = await this.repository.GetAssociationAsync(assignment);

            var expectedData = ExpectedData.VendorDistricts[0];
            Assert.Equivalent(expectedData, association, true);
            Assert.Equal(expectedData.Role, association.Role);
        }

        private async Task EnsureVendorDistrictResult(int vendorId, int districtId, string role)
        {
            using (var connection = this.fixture.Context.CreateConnection())
            {
                var vendDist = await connection.QuerySingleOrDefaultAsync<VendorDistrict>(
                    "SELECT * FROM VendorDistricts WHERE VendorID = @VendorId AND DistrictID = @DistrictId", new { vendorId, districtId });

                Assert.NotNull(vendDist);
                Assert.Equal(vendorId, vendDist.VendorId);
                Assert.Equal(districtId, vendDist.DistrictId);
                Assert.Equal(role, vendDist.Role);
            }
        }
    }
}
