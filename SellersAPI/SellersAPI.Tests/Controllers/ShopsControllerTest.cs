namespace SellersAPI.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using SellersAPI.Controllers;
    using SellersAPI.Interfaces;
    using SellersAPI.Mappers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShopsControllerTest
    {
        private readonly Mock<IShopsRepository> repository;
        private readonly ShopsController controller;

        public ShopsControllerTest()
        {
            this.repository = new Mock<IShopsRepository>();
            this.controller = new ShopsController(this.repository.Object);
        }

        [Theory]
        [InlineData(1)] // Ok with shop results
        [InlineData(3)] // Ok without any results
        public async Task GetAllByDistrictId_Ok(int districtId)
        {
            var expectedShops = ExpectedData.Shops.DeepClone()
                .Where(s => s.DistrictId == districtId)
                .Join(ExpectedData.ShopTypes, s => s.TypeId, t => t.Id, (s, t) => { s.ShopType = t; return s; });

            this.repository.Setup(repo => repo.GetAllByDistrictAsync(districtId)).ReturnsAsync(expectedShops);

            var result = await this.controller.GetAllByDistrictId(districtId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equivalent(expectedShops.Select(s => s.ToShopDTO()), okResult.Value);
        }

        [Fact]
        public async Task GetAllByDistrictId_BadRequest()
        {
            this.controller.ModelState.AddModelError("Error", "Model state is invalid");

            var result = await this.controller.GetAllByDistrictId(1);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetAllByDistrictId_StatusCode500()
        {
            this.repository.Setup(repo => repo.GetAllByDistrictAsync(1)).ThrowsAsync(new Exception("Error fetching data"));

            var result = await this.controller.GetAllByDistrictId(1);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error fetching data", statusCodeResult.Value);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            var id = 2;
            var expectedShop = ExpectedData.Shops[id - 1].DeepClone();
            expectedShop.ShopType = ExpectedData.ShopTypes[1];
            this.repository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(expectedShop);

            var result = await this.controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equivalent(expectedShop.ToShopDTO(), okResult.Value);
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
