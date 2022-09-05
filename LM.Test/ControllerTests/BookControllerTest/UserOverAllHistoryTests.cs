using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Response;
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
    public class UserOverAllHistoryTests
    {
        private readonly BookControllerFactory _fac;

        public UserOverAllHistoryTests()
        {
            this._fac = new BookControllerFactory();
        }


        [Fact]
        public async Task UserOverAllHistory_ModelEmptyCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.UserOverAllHistory(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UserOverAllHistory_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new pagiSearchVm();

            var resultModel = new ResultModel<PagedList<BookHistoryVM>>();
            _fac.TransactionService.Setup(x => x.UserOverAllHistory(It.IsAny<pagiSearchVm>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.UserOverAllHistory(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UserOverAllHistory_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new pagiSearchVm();

            var resultModel = new ResultModel<PagedList<BookHistoryVM>>("Error");
            _fac.TransactionService.Setup(x => x.UserOverAllHistory(It.IsAny<pagiSearchVm>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.UserOverAllHistory(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
