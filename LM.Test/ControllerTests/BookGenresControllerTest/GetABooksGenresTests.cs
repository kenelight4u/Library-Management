using LM.Domain.Common;
using LM.DTOs.Response.BookGenresVM;
using LM.DTOs.Response.BookVM;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LM.Test.ControllerTests.BookGenresControllerTest
{
    public class GetABooksGenresTests
    {
        private readonly BookGenresControllerFactory _fac;

        public GetABooksGenresTests()
        {
            this._fac = new BookGenresControllerFactory();
        }

        [Fact]
        public async Task GetABooksGenres_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var resultModel = new ResultModel<BookGenresVM>();
            _fac.BookGenreService.Setup(x => x.ViewABooksGenre(It.IsAny<Guid>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.GetABooksGenres(Guid.NewGuid()) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetABooksGenres_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var resultModel = new ResultModel<BookGenresVM>("Error");
            _fac.BookGenreService.Setup(x => x.ViewABooksGenre(It.IsAny<Guid>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.GetABooksGenres(Guid.NewGuid()) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetABooksGenres_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookGenreController.GetABooksGenres(Guid.Empty) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
