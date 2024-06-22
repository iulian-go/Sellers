namespace SellersAPI.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using SellersAPI.Controllers;
    using SellersAPI.Interfaces;

    public class DistrictsControllerTests
    {
        private readonly Mock<IDistrictsRepository> repository;
        private readonly DistrictsController controller;

        public DistrictsControllerTests()
        {
            this.repository = new Mock<IDistrictsRepository>();
            this.controller = new DistrictsController(this.repository.Object);
        }

        [Fact]
        public async Task GetAll_Ok()
        {
            this.repository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(ExpectedData.Districts);

            var result = await this.controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(ExpectedData.Districts, okResult.Value);
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
        public async Task GetById_Ok()
        {
            var id = 2;
            this.repository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(ExpectedData.Districts[id - 1]);

            var result = await this.controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(ExpectedData.Districts[id - 1], okResult.Value);
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
