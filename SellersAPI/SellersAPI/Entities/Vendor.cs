namespace SellersAPI.Entities
{
    public class Vendor
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<District> Districts { get; set; } = [];

        public string? Role { get; set; } = null;
    }
}
