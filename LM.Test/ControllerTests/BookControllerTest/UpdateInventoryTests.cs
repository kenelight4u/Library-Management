using LM.Domain.Common;
using LM.DTOs.Request.BookDTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LM.Test.ControllerTests.BookControllerTest
{
    public class UpdateInventoryTests
    {
        private readonly BookControllerFactory _fac;

        public UpdateInventoryTests()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task UpdateInventory_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new BookInventoryUpdateDTO();

            var resultModel = new ResultModel<string>();
            _fac.BookService.Setup(x => x.UpdateInventory(It.IsAny<BookInventoryUpdateDTO>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.UpdateInventory(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task UpdateInventory_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new BookInventoryUpdateDTO();

            var resultModel = new ResultModel<string>("Error");
            _fac.BookService.Setup(x => x.UpdateInventory(It.IsAny<BookInventoryUpdateDTO>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.UpdateInventory(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateInventory_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.UpdateInventory(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
