namespace SellersAPI.Entities
{
    public class District
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? City { get; set; }

        public List<Shop> Shops { get; set; } = [];

        public List<Vendor> Vendors { get; set; } = [];
    }
}
