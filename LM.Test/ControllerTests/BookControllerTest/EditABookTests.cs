using LM.Domain.Common;
using LM.DTOs.Request.BookDTO;
using LM.DTOs.Response.BookVM;
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
    public class EditABookTests
    {
        private readonly BookControllerFactory _fac;

        public EditABookTests()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task EditABook_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new EditBookDTO();

            var resultModel = new ResultModel<string>();
            _fac.BookService.Setup(x => x.EditABook(It.IsAny<EditBookDTO>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.EditABook(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task EditABook_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new EditBookDTO();

            var resultModel = new ResultModel<string>("Error");
            _fac.BookService.Setup(x => x.EditABook(It.IsAny<EditBookDTO>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.EditABook(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetABookByID_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.EditABook(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
