using LM.Domain.Common.ViewModel;
using LM.Domain.Common;
using LM.Domain.Utils.Pagination;
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
    public class GetABookByIDTests
    {
        private readonly BookControllerFactory _fac;

        public GetABookByIDTests()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task GetABookByID_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var resultModel = new ResultModel<BookVM>();
            _fac.BookService.Setup(x => x.GetABookByID(It.IsAny<Guid>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.GetABookByID(Guid.NewGuid()) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetABookByID_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var resultModel = new ResultModel<BookVM>("Error");
            _fac.BookService.Setup(x => x.GetABookByID(It.IsAny<Guid>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.GetABookByID(Guid.NewGuid()) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetABookByID_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.GetABookByID(Guid.Empty) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
