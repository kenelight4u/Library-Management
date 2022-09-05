using LM.Domain.Common;
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
    public class DeleteBookGenreTests
    {
        private readonly BookGenresControllerFactory _fac;

        public DeleteBookGenreTests()
        {
            this._fac = new BookGenresControllerFactory();
        }

        [Fact]
        public async Task DeleteBookGenre_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = Guid.NewGuid();

            var resultModel = new ResultModel<string>();
            _fac.BookGenreService.Setup(x => x.DeleteABookGenre(It.IsAny<Guid>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.DeleteBookGenre(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteBookGenre_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = Guid.NewGuid();

            var resultModel = new ResultModel<string>("Error");
            _fac.BookGenreService.Setup(x => x.DeleteABookGenre(It.IsAny<Guid>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.DeleteBookGenre(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteBookGenre_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookGenreController.DeleteBookGenre(Guid.Empty) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
