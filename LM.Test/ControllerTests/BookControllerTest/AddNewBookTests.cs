using LM.Domain.Common;
using LM.DTOs.Request.BookDTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LM.Test.ControllerTests.BookControllerTest
{
    public class AddNewBookTests
    {
        private readonly BookControllerFactory _fac;

        public AddNewBookTests()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task CreateABook_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.AddNewBook(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task CreateABook_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new BookDTO()
            {
                Author = "Test",
                BookGenresId = Guid.NewGuid(),
                Description = "Test Case",
                ISBN = "6789567",
            };

            var resultModel = new ResultModel<string>();
            _fac.BookService.Setup(x => x.AddNewBook(It.IsAny<BookDTO>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.AddNewBook(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task CreateABook_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new BookDTO() {};

            var resultModel = new ResultModel<string>("Error Occured");
            _fac.BookService.Setup(x => x.AddNewBook(It.IsAny<BookDTO>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.AddNewBook(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
