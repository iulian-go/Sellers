namespace SellersAPI.Interfaces
{
    using SellersAPI.Entities;

    public interface IVendorsRepository
    {
        public Task<IEnumerable<Vendor>> GetAllAsync();

        public Task<IEnumerable<Vendor>> GetAllByDistrictAsync(int districtId);

        public Task<Vendor> GetByIdAsync(int id);
    }
}
