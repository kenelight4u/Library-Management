using LM.API.Controllers.v1;
using LM.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Test.ControllerTests.BookControllerTest
{
    public class BookControllerFactory
    {
        public Mock<IBookService> BookService = new Mock<IBookService>();

        public BookControllerFactory()
        {
            BookController = new BookController(BookService.Object);

            BookController.ControllerContext.HttpContext =
                new DefaultHttpContext { User = TestData.GetAuthenticatedUser() };
        }

        public BookController BookController { get; set; }
    }
}
