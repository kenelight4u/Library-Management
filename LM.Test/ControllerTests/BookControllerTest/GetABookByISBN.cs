using LM.Domain.Common;
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
    public class GetABookByISBN
    {
        private readonly BookControllerFactory _fac;

        public GetABookByISBN()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task GetABookByISBN_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var resultModel = new ResultModel<BookVM>();
            _fac.BookService.Setup(x => x.GetABookByISBN(It.IsAny<string>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.GetABookByISBN("5678") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetABookByISBN_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var resultModel = new ResultModel<BookVM>("Error");
            _fac.BookService.Setup(x => x.GetABookByISBN(It.IsAny<string>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.GetABookByISBN("thygf") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetABookByISBN_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.GetABookByISBN(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
