using LM.Domain.Common;
using LM.DTOs.Request.BookDTO;
using LM.DTOs.Request.BookGenreDTO;
using LM.Test.ControllerTests.BookControllerTest;
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
    public class AddBookGenreTests
    {
        private readonly BookGenresControllerFactory _fac;

        public AddBookGenreTests()
        {
            this._fac = new BookGenresControllerFactory();
        }

        [Fact]
        public async Task AddBookGenre_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookGenreController.AddBookGenre(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task AddBookGenre_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new BookGenresDTO()
            {
                Name = "Slave",
                Description = "A slave Genres"
            };

            var resultModel = new ResultModel<string>();
            _fac.BookGenreService.Setup(x => x.AddBookGenre(It.IsAny<BookGenresDTO>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.AddBookGenre(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task AddBookGenre_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new BookGenresDTO { };

            var resultModel = new ResultModel<string>("Error Occured");
            _fac.BookGenreService.Setup(x => x.AddBookGenre(It.IsAny<BookGenresDTO>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.AddBookGenre(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
