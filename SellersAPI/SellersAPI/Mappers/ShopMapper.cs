namespace SellersAPI.Mappers
{
    using SellersAPI.DTOs;
    using SellersAPI.Entities;

    public static class ShopMapper
    {
        public static ShopDTO ToShopDTO(this Shop shop)
        {
            return new ShopDTO
            {
                Id = shop.Id,
                Name = shop.Name,
                Address = shop.Address,
                ShopType = shop.ShopType.Name
            };
        }
    }
}
