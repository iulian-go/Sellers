namespace SellersAPI.Tests.Repositories
{
    using SellersAPI.Repositories;

    public class DistrictsRepositoryTests : IClassFixture<TestFixture>
    {
        private readonly DistrictsRepository repository;

        public DistrictsRepositoryTests(TestFixture fixture)
        {
            this.repository = new DistrictsRepository(fixture.Context);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            var districts = await this.repository.GetAllAsync();

            Assert.Equivalent(ExpectedData.Districts, districts, true);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            var id = 2;
            var district = await this.repository.GetByIdAsync(id);

            Assert.Equivalent(ExpectedData.Districts[id - 1], district);
        }
    }
}
