using LM.Domain.Entities;
using LM.DTOs.Request.BookDTO;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LM.Test.BookTests
{
    public class AddNewBookTests
    {
        private readonly BookServiceFactory _fac;

        public AddNewBookTests()
        {
            _fac = new BookServiceFactory();
        }

        [Fact]
        public void AddNewBook_ShouldReturnError_WhenBookGenreIdIsInvalid()
        {
            // Arrange
            Guid invalidId = Guid.Parse("b062469f-3d58-48b8-8626-4a7a629d3831");

            var model = new BookDTO { Author = "Ekene Test", BookGenresId = invalidId };
            var bookGenres = TestData.GetBookGenres();
            var books = TestData.GetBooks();

            _fac.BookGenreRepository.Setup(x => x.DataStore.GetAllQuery())
            .Equals(bookGenres.FirstOrDefault(x => x.ID == invalidId));

            //Act
            var result = _fac.BookService.AddNewBook(model);

            //Assert
            _fac.BookGenreRepository.Verify(x => x.DataStore.GetAllQuery(), Times.Once);
            _fac.UnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);

            Assert.NotNull(result);
        }
    }
}
