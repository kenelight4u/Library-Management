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

namespace LM.Test.BookGenreTests
{
    public class BookGenreServiceFactory
    {
        public Mock<IStoreManager<BookGenres>> BookGenreRepository = new Mock<IStoreManager<BookGenres>>();
        public Mock<IUnitOfWork> UnitOfWork = new Mock<IUnitOfWork>();
        public Mock<IContextAccessor> ContextAccessor = new Mock<IContextAccessor>();

        public BookGenreServiceFactory()
        {
            BookGenreService = new BookGenreService(
                BookGenreRepository.Object,
                ContextAccessor.Object,
                UnitOfWork.Object
                );

            ContextAccessor.Setup(x => x.GetCurrentUserId()).Returns(new UserPrincipal(TestData.GetAuthenticatedUser()).UserId);
        }

        public BookGenreService BookGenreService { get; set; }
    }
}
