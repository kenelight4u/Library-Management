using LM.Domain.Common;
using LM.DTOs.Request.BookGenreDTO;
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
    public class UpdateBookGenreTests
    {
        private readonly BookGenresControllerFactory _fac;

        public UpdateBookGenreTests()
        {
            this._fac = new BookGenresControllerFactory();
        }

        [Fact]
        public async Task UpdateBookGenre_ModelNullCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookGenreController.UpdateBookGenre(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateBookGenre_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new EditBookGenresDTO()
            {
                Name = "Slave",
                Description = "A slave Genres"
            };

            var resultModel = new ResultModel<string>();
            _fac.BookGenreService.Setup(x => x.EditABookGenre(It.IsAny<EditBookGenresDTO>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.UpdateBookGenre(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateBookGenre_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new EditBookGenresDTO { };

            var resultModel = new ResultModel<string>("Error Occured");
            _fac.BookGenreService.Setup(x => x.EditABookGenre(It.IsAny<EditBookGenresDTO>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.UpdateBookGenre(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
