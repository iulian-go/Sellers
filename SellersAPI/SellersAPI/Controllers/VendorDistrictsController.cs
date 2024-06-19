namespace SellersAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SellersAPI.DTOs;
    using SellersAPI.Interfaces;

    [Route("api/assignments")]
    [ApiController]
    public class VendorDistrictsController : ControllerBase
    {
        private readonly IVendorDistrictRepository repository;

        public VendorDistrictsController(IVendorDistrictRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AssignVendor([FromBody]AssignmentDTO assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // Needs validation if vendor or district do not exist

                var result = await this.repository.AssignVendorAsync(assignment);

                if (result == 0) return StatusCode(500, "Assignment failed");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangePrimary([FromBody] AssignmentDTO assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await this.repository.ChangePrimaryAsync(assignment);

                if (result == 0) return StatusCode(500, "Change failed");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveVendor([FromBody] AssignmentDTO assignment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var association = await this.repository.GetAssociationAsync(assignment);

                if (association == null) return NotFound();

                var result = await this.repository.RemoveVenorAsync(assignment);

                if (result == 0) return StatusCode(500, "Remove failed");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
