namespace SellersAPI.Tests
{
    using Microsoft.Extensions.Configuration;
    using SellersAPI.Interfaces;

    public class TestFixture
    {
        public TestFixture()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            this.Context = new TestDbContext(configuration);
        }

        public IDbContext Context { get; set; }
    }
}
