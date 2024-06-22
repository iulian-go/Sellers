namespace SellersAPI.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using SellersAPI.Controllers;
    using SellersAPI.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class VendorsControllerTest
    {
        private readonly Mock<IVendorsRepository> repository;
        private readonly VendorsController controller;

        public VendorsControllerTest()
        {
            this.repository = new Mock<IVendorsRepository>();
            this.controller = new VendorsController(this.repository.Object);
        }

        [Fact]
        public async Task GetAll_Ok()
        {
            this.repository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(ExpectedData.Vendors);

            var result = await this.controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(ExpectedData.Vendors, okResult.Value);
        }

        [Fact]
        public async Task GetAll_BadRequest()
        {
            this.controller.ModelState.AddModelError("Error", "Model state is invalid");

            var result = await this.controller.GetAll();

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetAll_StatusCode500()
        {
            this.repository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Error fetching data"));

            var result = await this.controller.GetAll();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error fetching data", statusCodeResult.Value);
        }

        [Fact]
        public async Task GetAllByDistrictId_Ok()
        {
            var districtId = 2;
            var expectedVendors = ExpectedData.VendorDistricts
                .Where(d => d.DistrictId == districtId)
                .Join(ExpectedData.Vendors, vd => vd.VendorId, v => v.Id, (vd, v) => v);
            this.repository.Setup(repo => repo.GetAllByDistrictAsync(districtId)).ReturnsAsync(expectedVendors);

            var result = await this.controller.GetAllByDistrictId(districtId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedVendors, okResult.Value);
        }

        [Fact]
        public async Task GetAllByDistrictId_BadRequest()
        {
            this.controller.ModelState.AddModelError("Error", "Model state is invalid");

            var result = await this.controller.GetAllByDistrictId(2);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetAllByDistrictId_StatusCode500()
        {
            this.repository.Setup(repo => repo.GetAllByDistrictAsync(2)).ThrowsAsync(new Exception("Error fetching data"));

            var result = await this.controller.GetAllByDistrictId(2);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error fetching data", statusCodeResult.Value);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            var id = 2;
            this.repository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(ExpectedData.Vendors[id - 1]);

            var result = await this.controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(ExpectedData.Vendors[id - 1], okResult.Value);
        }

        [Fact]
        public async Task GetById_BadRequest()
        {
            this.controller.ModelState.AddModelError("Error", "Model state is invalid");

            var result = await this.controller.GetById(2);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetById_StatusCode500()
        {
            this.repository.Setup(repo => repo.GetByIdAsync(2)).ThrowsAsync(new Exception("Error fetching data"));

            var result = await this.controller.GetById(2);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error fetching data", statusCodeResult.Value);
        }
    }
}
