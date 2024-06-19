namespace SellersAPI.Interfaces
{
    using SellersAPI.DTOs;
    using SellersAPI.Entities;

    public interface IVendorDistrictRepository
    {
        public Task<int> AssignVendorAsync(AssignmentDTO assignment);

        public Task<int> ChangePrimaryAsync(AssignmentDTO assignment);

        public Task<int> RemoveVenorAsync(AssignmentDTO assignment);

        public Task<VendorDistrict> GetAssociationAsync(AssignmentDTO assignment);
    }
}
