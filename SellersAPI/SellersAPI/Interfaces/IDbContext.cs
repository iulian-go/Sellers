namespace SellersAPI.Interfaces
{
    using System.Data;

    public interface IDbContext
    {
        public IDbConnection CreateConnection();
    }
}
