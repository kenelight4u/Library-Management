using LM.Domain.Entities;
using LM.Test.BookGenreTests;
using LM.Test.BookTests;
using LM.Test.TransactionTests;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Test
{
    public static class Setup
    {
        private static readonly Mock<IQueryable<Book>> MockBooks =
            (Mock<IQueryable<Book>>)TestData.GetBooks().AsQueryable().BuildMock();

        private static readonly Mock<IQueryable<BookGenres>> MockBookGenres =
            (Mock<IQueryable<BookGenres>>)TestData.GetBookGenres().AsQueryable().BuildMock();

        private static readonly Mock<IQueryable<BookInventory>> MockBookInventory =
            (Mock<IQueryable<BookInventory>>)TestData.GetBookInventories().AsQueryable().BuildMock();

        private static readonly Mock<IQueryable<BookHistory>> MockBookHistory =
            (Mock<IQueryable<BookHistory>>)TestData.GetBookHistories().AsQueryable().BuildMock();

        public static BookServiceFactory BookServiceFactory(BookServiceFactory fac)
        {
            fac.BookRepository.Setup(x => x.DataStore.GetAllQuery()).Returns(MockBooks.Object);
            fac.BookGenreRepository.Setup(x => x.DataStore.GetAllQuery()).Returns(MockBookGenres.Object);
            fac.BookInventoryRepository.Setup(x => x.DataStore.GetAllQuery()).Returns(MockBookInventory.Object);

            fac.UnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            return fac;
        }

        public static BookGenreServiceFactory BookGenreServiceFactory(BookGenreServiceFactory fac)
        {
            fac.BookGenreRepository.Setup(x => x.DataStore.GetAllQuery()).Returns(MockBookGenres.Object);

            fac.UnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            return fac;
        }

        public static TransactionServiceFactory TransactionServiceFactory(TransactionServiceFactory fac)
        {
            fac.BookRepository.Setup(x => x.DataStore.GetAllQuery()).Returns(MockBooks.Object);
            fac.BookHistoryRepository.Setup(x => x.DataStore.GetAllQuery()).Returns(MockBookHistory.Object);

            fac.UnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            return fac;
        }
    }
}
