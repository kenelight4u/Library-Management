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
    public class DeleteBookTests
    {
        private readonly BookControllerFactory _fac;

        public DeleteBookTests()
        {
            this._fac = new BookControllerFactory();
        }


        [Fact]
        public async Task DeleteABook_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = Guid.NewGuid();

            var resultModel = new ResultModel<string>();
            _fac.BookService.Setup(x => x.DeleteABook(It.IsAny<Guid>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.DeleteBook(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteABook_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = Guid.NewGuid();

            var resultModel = new ResultModel<string>("Error");
            _fac.BookService.Setup(x => x.DeleteABook(It.IsAny<Guid>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.DeleteBook(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteABook_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.DeleteBook(Guid.Empty) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
