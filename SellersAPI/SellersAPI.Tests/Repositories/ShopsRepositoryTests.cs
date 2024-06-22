namespace SellersAPI.Tests.Repositories
{
    using SellersAPI.Repositories;
    using System.Linq;

    public class ShopsRepositoryTests : IClassFixture<TestFixture>
    {
        private readonly ShopsRepository repository;

        public ShopsRepositoryTests(TestFixture fixture)
        {
            this.repository = new ShopsRepository(fixture.Context);
        }

        [Fact]
        public async Task GetAllByDistrictAsync()
        {
            var districtId = 1;
            var shops = await this.repository.GetAllByDistrictAsync(districtId);

            var expectedData = ExpectedData.Shops.DeepClone()
                .Where(s => s.DistrictId == districtId)
                .Join(ExpectedData.ShopTypes, s => s.TypeId, t => t.Id, (s, t) => { s.ShopType = t; return s; });
            Assert.Equivalent(expectedData, shops, true);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            var id = 2;
            var shop = await this.repository.GetByIdAsync(id);

            var expectedShop = ExpectedData.Shops[id - 1].DeepClone();
            expectedShop.ShopType = ExpectedData.ShopTypes[1];
            Assert.Equivalent(expectedShop, shop);
        }
    }
}
