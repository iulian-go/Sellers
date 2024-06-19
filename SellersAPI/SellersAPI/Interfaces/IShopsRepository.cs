namespace SellersAPI.Interfaces
{
    using SellersAPI.Entities;

    public interface IShopsRepository
    {
        public Task<IEnumerable<Shop>> GetAllByDistrictAsync(int districtId);

        public Task<Shop> GetByIdAsync(int id);
    }
}
