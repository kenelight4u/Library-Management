using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
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
    public class GetAllBooksTests
    {
        private readonly BookControllerFactory _fac;

        public GetAllBooksTests()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task GetAllBooks_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new SearchVM() { };

            var resultModel = new ResultModel<PagedList<BookVM>>();
            _fac.BookService.Setup(x => x.GetAllBooks(It.IsAny<SearchVM>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.GetAllBooks(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllBooks_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new SearchVM() { };

            var resultModel = new ResultModel<PagedList<BookVM>>("Error Occured");
            _fac.BookService.Setup(x => x.GetAllBooks(It.IsAny<SearchVM>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.GetAllBooks(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }

    
}
