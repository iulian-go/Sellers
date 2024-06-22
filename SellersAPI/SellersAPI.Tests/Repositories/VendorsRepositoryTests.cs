namespace SellersAPI.Tests.Repositories
{
    using SellersAPI.Entities;
    using SellersAPI.Repositories;
    using System.Linq;

    public class VendorsRepositoryTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture fixture;
        private readonly VendorsRepository repository;

        public VendorsRepositoryTests(TestFixture fixture)
        {
            this.fixture = fixture;
            this.repository = new VendorsRepository(fixture.Context);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            var vendors = await this.repository.GetAllAsync();

            var expectedData = ExpectedData.Vendors.DeepClone()
                .GroupJoin(ExpectedData.VendorDistricts, v => v.Id, vd => vd.VendorId,
                    (v, vdList) =>
                    {
                        if (vdList != null)
                        {
                            v.Districts.AddRange(vdList.Select(vd => new District { Id = vd.DistrictId }));
                            v.Role ??= vdList.OrderBy(vd => vd.Role).FirstOrDefault()?.Role;
                        }
                        return v;
                    })
                .OrderBy(v => v.Role);
            Assert.Equivalent(expectedData, vendors, true);
        }

        [Fact]
        public async Task GetAllByDistrictAsync()
        {
            var districtId = 2;
            var vendors = await this.repository.GetAllByDistrictAsync(districtId);

            var expectedData = ExpectedData.VendorDistricts.DeepClone()
                .Where(d => d.DistrictId == districtId)
                .Join(ExpectedData.Vendors, vd => vd.VendorId, v => v.Id,
                    (vd, v) =>
                    {
                        v.Role = vd.Role;
                        return v;
                    })
                .OrderBy(v => v?.Role);
            Assert.Equivalent(expectedData, vendors, true);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            var id = 4;
            var vendor = await this.repository.GetByIdAsync(id);

            var expectedVendor =  ExpectedData.Vendors[id - 1].DeepClone();
            expectedVendor.Role = "secondary";
            Assert.Equivalent(expectedVendor, vendor);
        }
    }
}
