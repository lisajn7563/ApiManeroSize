using ApiManeroSize.Contexts;
using ApiManeroSize.Controllers;
using ApiManeroSize.Entites;
using ApiManeroSize.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSizeApi;

public class SizeControllerTests
{
    private DbContextOptions<DataContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task Create_ShouldAddSize_IfModelStateIsValid()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            var controller = new SizeController(context);
            var model = new SizeRegistration
            {
                id = "1",
                SizeTitle = "Large"
            };

            // Act
            var result = await controller.Create(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var createdSize = Assert.IsType<SizeEntity>(okResult.Value);
            Assert.Equal("Large", createdSize.sizeTitle);

            // Verify that the size was added to the database
            var sizeInDb = await context.Size.FindAsync("1");
            Assert.NotNull(sizeInDb);
            Assert.Equal("Large", sizeInDb.sizeTitle);
        }
    }
    [Fact]
    public async Task Create_ShouldReturnBadRequest_IfModelStateIsInvalid()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            var controller = new SizeController(context);
            controller.ModelState.AddModelError("Error", "ModelState is invalid");

            var model = new SizeRegistration
            {
                id = "1",
                SizeTitle = "Large"
            };

            // Act
            var result = await controller.Create(model);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllSizes()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            context.Size.AddRange(new List<SizeEntity>
                {
                    new SizeEntity { Id = "1", sizeTitle = "Small" },
                    new SizeEntity { Id = "2", sizeTitle = "Medium" }
                });
            context.SaveChanges();

            var controller = new SizeController(context);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var sizeList = Assert.IsType<List<SizeEntity>>(okResult.Value);
            Assert.Equal(2, sizeList.Count);
        }
    }
}
