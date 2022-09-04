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
    public class ViewInventoryTests
    {
        private readonly BookControllerFactory _fac;

        public ViewInventoryTests()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task ViewInventry_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new pagiSearchVm() { };

            var resultModel = new ResultModel<PagedList<BookInventoryVM>>();
            _fac.BookService.Setup(x => x.GetAllBooksInventory(It.IsAny<pagiSearchVm>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.ViewInventory(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ViewInventry_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new pagiSearchVm() { };

            var resultModel = new ResultModel<PagedList<BookInventoryVM>>("Error Occured");
            _fac.BookService.Setup(x => x.GetAllBooksInventory(It.IsAny<pagiSearchVm>()))
            .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.ViewInventory(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
