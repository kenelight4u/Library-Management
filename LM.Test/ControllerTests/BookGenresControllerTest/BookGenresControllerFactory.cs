using LM.API.Controllers.v1;
using LM.Application.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Test.ControllerTests.BookGenresControllerTest
{
    public class BookGenresControllerFactory
    {
        public Mock<IBookGenresService> BookGenreService = new Mock<IBookGenresService>();

        public BookGenresControllerFactory()
        {
            BookGenreController = new BookGenresController(BookGenreService.Object);
        }

        public BookGenresController BookGenreController { get; set; }
    }
}
