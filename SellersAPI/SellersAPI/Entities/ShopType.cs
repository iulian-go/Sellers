namespace SellersAPI.Entities
{
    public class ShopType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Shop> Shops { get; set; }
    }
}
