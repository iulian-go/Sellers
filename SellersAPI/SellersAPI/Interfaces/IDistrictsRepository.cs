namespace SellersAPI.Interfaces
{
    using SellersAPI.Entities;

    public interface IDistrictsRepository
    {
        public Task<IEnumerable<District>> GetAllAsync();

        public Task<District> GetByIdAsync(int id);
    }
}
