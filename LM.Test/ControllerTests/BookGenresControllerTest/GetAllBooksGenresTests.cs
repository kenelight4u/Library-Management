using LM.Domain.Common.ViewModel;
using LM.Domain.Common;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Response.BookVM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LM.DTOs.Response.BookGenresVM;
using Moq;

namespace LM.Test.ControllerTests.BookGenresControllerTest
{
    public class GetAllBooksGenresTests
    {
        private readonly BookGenresControllerFactory _fac;

        public GetAllBooksGenresTests()
        {
            this._fac = new BookGenresControllerFactory();
        }

        [Fact]
        public async Task GetAllBooksGenres_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new SearchVM() { };

            var resultModel = new ResultModel<PagedList<BookGenresVM>>();
            _fac.BookGenreService.Setup(x => x.ViewAllBooksGenres(It.IsAny<SearchVM>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.GetAllBooksGenres(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllBooksGenres_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new SearchVM() { };

            var resultModel = new ResultModel<PagedList<BookGenresVM>>("Error Occured");
            _fac.BookGenreService.Setup(x => x.ViewAllBooksGenres(It.IsAny<SearchVM>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookGenreController.GetAllBooksGenres(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
