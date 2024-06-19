namespace SellersAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SellersAPI.Interfaces;

    [Route("api/vendors")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorsRepository vendorsRepository;

        public VendorsController(IVendorsRepository vendorsRepository)
        {
            this.vendorsRepository = vendorsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var vendors = await this.vendorsRepository.GetAllAsync();
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("bydistrict/{id:int}")]
        public async Task<IActionResult> GetAllByDistrictId([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var vendors = await this.vendorsRepository.GetAllByDistrictAsync(id);
                return Ok(vendors);
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
                var vendor = await this.vendorsRepository.GetByIdAsync(id);

                if (vendor == null) return NotFound();

                return Ok(vendor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
