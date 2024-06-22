namespace SellersAPI.Repositories
{
    using Dapper;
    using SellersAPI.Context;
    using SellersAPI.Entities;
    using SellersAPI.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DistrictsRepository : IDistrictsRepository
    {
        private readonly IDbContext context;

        public DistrictsRepository(IDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<District>> GetAllAsync()
        {
            var query = "SELECT * FROM Districts";

            using (var connection = this.context.CreateConnection())
            {
                var districts = await connection.QueryAsync<District>(query);
                return districts.ToList();
            }
        }

        public async Task<District> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Districts WHERE Id = @Id";

            using (var connection = this.context.CreateConnection())
            {
                var district = await connection.QuerySingleOrDefaultAsync<District>(query, new { id });
                return district;
            }
        }
    }
}
