namespace SellersAPI.Repositories
{
    using Dapper;
    using SellersAPI.Context;
    using SellersAPI.Entities;
    using SellersAPI.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ShopsRepository : IShopsRepository
    {
        private readonly IDbContext context;

        public ShopsRepository(IDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Shop>> GetAllByDistrictAsync(int districtId)
        {
            var query = @"SELECT s.*, t.ID, t.Name 
                          FROM Shops s 
                          JOIN ShopTypes t ON s.TypeID = t.ID 
                          WHERE DistrictId = @districtId";

            using (var connection = this.context.CreateConnection())
            {
                var shops = await connection.QueryAsync<Shop, ShopType, Shop>(query,
                    (shop, shopType) => {
                        shop.ShopType = shopType;
                        return shop;
                    },
                    splitOn: "ID", param: new { districtId });

                return shops.ToList();
            }
        }

        public async Task<Shop> GetByIdAsync(int id)
        {
            var query = @"SELECT s.*, t.ID, t.Name 
                          FROM Shops s 
                          JOIN ShopTypes t ON s.TypeID = t.ID 
                          WHERE s.ID = @Id";

            using (var connection = this.context.CreateConnection())
            {
                var shop = await connection.QueryAsync<Shop, ShopType, Shop>(query,
                    (shop, shopType) => {
                        shop.ShopType = shopType;
                        return shop;
                    },
                    splitOn: "ID", param: new { id });
                return shop.FirstOrDefault();
            }
        }
    }
}
