namespace SellersAPI.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using SellersAPI.Controllers;
    using SellersAPI.DTOs;
    using SellersAPI.Entities;
    using SellersAPI.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VendorDistrictsControllerTests
    {
        private readonly Mock<IVendorDistrictRepository> repository;
        private readonly VendorDistrictsController controller;

        public VendorDistrictsControllerTests()
        {
            this.repository = new Mock<IVendorDistrictRepository>();
            this.controller = new VendorDistrictsController(this.repository.Object);
        }

        [Fact]
        public async Task AssignVendor_Ok()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            this.repository.Setup(repo => repo.AssignVendorAsync(assignment)).ReturnsAsync(1);

            var result = await this.controller.AssignVendor(assignment);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task AssignVendor_BadRequest()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            this.controller.ModelState.AddModelError("Error", "Model state is invalid");

            var result = await this.controller.AssignVendor(assignment);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetAll_StatusCode500_Exception()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            this.repository.Setup(repo => repo.AssignVendorAsync(assignment)).ThrowsAsync(new Exception("Error fetching data"));

            var result = await this.controller.AssignVendor(assignment);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error fetching data", statusCodeResult.Value);
        }

        [Fact]
        public async Task GetAll_StatusCode500_Failed()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            this.repository.Setup(repo => repo.AssignVendorAsync(assignment)).ReturnsAsync(0);

            var result = await this.controller.AssignVendor(assignment);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Assignment failed", statusCodeResult.Value);
        }

        [Fact]
        public async Task ChangePrimary_Ok()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            this.repository.Setup(repo => repo.ChangePrimaryAsync(assignment)).ReturnsAsync(2);

            var result = await this.controller.ChangePrimary(assignment);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task ChangePrimary_BadRequest()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            this.controller.ModelState.AddModelError("Error", "Model state is invalid");

            var result = await this.controller.ChangePrimary(assignment);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task ChangePrimary_StatusCode500_Exception()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            this.repository.Setup(repo => repo.ChangePrimaryAsync(assignment)).ThrowsAsync(new Exception("Error fetching data"));

            var result = await this.controller.ChangePrimary(assignment);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error fetching data", statusCodeResult.Value);
        }

        [Fact]
        public async Task ChangePrimary_StatusCode500_Failed()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 7, DistrictId = 1 };
            this.repository.Setup(repo => repo.ChangePrimaryAsync(assignment)).ReturnsAsync(0);

            var result = await this.controller.ChangePrimary(assignment);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Change failed", statusCodeResult.Value);
        }

        [Fact]
        public async Task RemoveVendor_Ok()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 2, DistrictId = 1 };
            this.repository.Setup(repo => repo.GetAssociationAsync(assignment)).ReturnsAsync(ExpectedData.VendorDistricts[2]);
            this.repository.Setup(repo => repo.RemoveVenorAsync(assignment)).ReturnsAsync(1);

            var result = await this.controller.RemoveVendor(assignment);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task RemoveVendor_BadRequest()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 2, DistrictId = 1 };
            this.controller.ModelState.AddModelError("Error", "Model state is invalid");

            var result = await this.controller.RemoveVendor(assignment);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task RemoveVendor_StatusCode500_Exception()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 2, DistrictId = 1 };
            this.repository.Setup(repo => repo.GetAssociationAsync(assignment)).ReturnsAsync(ExpectedData.VendorDistricts[2]);
            this.repository.Setup(repo => repo.RemoveVenorAsync(assignment)).ThrowsAsync(new Exception("Error fetching data"));

            var result = await this.controller.RemoveVendor(assignment);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Error fetching data", statusCodeResult.Value);
        }

        [Fact]
        public async Task RemoveVendor_StatusCode500_Failed()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 2, DistrictId = 1 };
            this.repository.Setup(repo => repo.GetAssociationAsync(assignment)).ReturnsAsync(ExpectedData.VendorDistricts[2]);
            this.repository.Setup(repo => repo.RemoveVenorAsync(assignment)).ReturnsAsync(0);

            var result = await this.controller.RemoveVendor(assignment);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Remove failed", statusCodeResult.Value);
        }

        [Fact]
        public async Task RemoveVendor_NotFound()
        {
            AssignmentDTO assignment = new AssignmentDTO { VendorId = 2, DistrictId = 1 };
            this.repository.Setup(repo => repo.GetAssociationAsync(assignment)).ReturnsAsync(() => null);

            var result = await this.controller.RemoveVendor(assignment);

            var statusCodeResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
        }
    }
}
