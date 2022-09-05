using LM.Domain.Common;
using LM.DTOs.Request.BookDTO;
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
    public class IssueBookTests
    {
        private readonly BookControllerFactory _fac;

        public IssueBookTests()
        {
            this._fac = new BookControllerFactory();
        }

        [Fact]
        public async Task IssueBook_ModelEmptyCase()
        {
            // Arrange

            // Act
            var result = await _fac.BookController.IssueBook(Guid.Empty) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task IssueBook_ValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = Guid.NewGuid();
            
            var resultModel = new ResultModel<IssueBookVm>();
            _fac.TransactionService.Setup(x => x.IssueBook(It.IsAny<Guid>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.IssueBook(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task IssueBook_InValidCase()
        {
            // Arrange
            var userId = TestData.UserId;

            var model = Guid.Parse("7969a9ac-c081-4b75-a7fc-51e64c79c8dc");

            var resultModel = new ResultModel<IssueBookVm>("Error");
            _fac.TransactionService.Setup(x => x.IssueBook(It.IsAny<Guid>()))
                .Returns(Task.FromResult(resultModel));

            // Act
            var result = await _fac.BookController.IssueBook(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
