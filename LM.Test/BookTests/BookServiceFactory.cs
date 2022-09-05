using LM.Application.Interfaces.Persistence;
using LM.Application.Interfaces.Utilities;
using LM.Domain.Common;
using LM.Domain.Entities;
using LM.Services.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Test.BookTests
{
    public class BookServiceFactory
    {
        public Mock<IStoreManager<BookGenres>> BookGenreRepository = new Mock<IStoreManager<BookGenres>>();
        public Mock<IStoreManager<Book>> BookRepository = new Mock<IStoreManager<Book>>();
        public Mock<IStoreManager<BookInventory>> BookInventoryRepository = new Mock<IStoreManager<BookInventory>>();
        public Mock<IUnitOfWork> UnitOfWork = new Mock<IUnitOfWork>();
        public Mock<IContextAccessor> ContextAccessor = new Mock<IContextAccessor>();

        public BookServiceFactory()
        {
            BookService = new BookService(
                ContextAccessor.Object,
                BookInventoryRepository.Object,
                BookRepository.Object,
                BookGenreRepository.Object,
                UnitOfWork.Object
                );

            ContextAccessor.Setup(x => x.GetCurrentUserId()).Returns(new UserPrincipal(TestData.GetAuthenticatedUser()).UserId);
        }

        public BookService BookService { get; set; }
    }
}
