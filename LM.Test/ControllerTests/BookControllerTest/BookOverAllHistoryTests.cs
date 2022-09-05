using LM.Domain.Common.ViewModel;
using LM.Domain.Common;
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
    public class BookOverAllHistoryTests
    {
        private readonly BookControllerFactory _fac;

        public BookOverAllHistoryTests()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task BookOverAllHistory_ModelEmptyCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.BookOverAllHistory(null, Guid.Empty) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task BookOverAllHistory_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new pagiSearchVm();

            var resultModel = new ResultModel<PagedList<BookHistVm>>();
            _fac.TransactionService.Setup(x => x.BookOverAllHistory(It.IsAny<pagiSearchVm>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.BookOverAllHistory(model, userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task BookOverAllHistory_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = new pagiSearchVm();

            var resultModel = new ResultModel<PagedList<BookHistVm>>("Error");
            _fac.TransactionService.Setup(x => x.BookOverAllHistory(It.IsAny<pagiSearchVm>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.BookOverAllHistory(model, userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
