namespace SellersAPI.Entities
{
    public class Shop
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int DistrictId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ShopType ShopType { get; set; }
    }
}
