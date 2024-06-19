namespace SellersAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SellersAPI.Interfaces;

    [Route("api/districts")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IDistrictsRepository districtsRepository;

        public DistrictsController(IDistrictsRepository districtsRepository)
        {
            this.districtsRepository = districtsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var districts = await this.districtsRepository.GetAllAsync();
                return Ok(districts);
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
                var district = await this.districtsRepository.GetByIdAsync(id);

                if (district == null) return NotFound();

                return Ok(district);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
