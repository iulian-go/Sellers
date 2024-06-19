namespace SellersAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SellersAPI.Interfaces;
    using SellersAPI.Mappers;

    [Route("api/shops")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IShopsRepository shopsRepository;

        public ShopsController(IShopsRepository shopsRepository)
        {
            this.shopsRepository = shopsRepository;
        }

        [HttpGet("bydistrict/{id:int}")]
        public async Task<IActionResult> GetAllByDistrictId([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var shops = await this.shopsRepository.GetAllByDistrictAsync(id);
                var shopDTOs = shops.Select(s => s.ToShopDTO());

                return Ok(shopDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var shop = await this.shopsRepository.GetByIdAsync(id);

                if (shop == null) return NotFound();

                return Ok(shop.ToShopDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
