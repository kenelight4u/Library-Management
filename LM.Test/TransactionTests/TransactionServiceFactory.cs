using LM.Application.Interfaces.Persistence;
using LM.Application.Interfaces.Utilities;
using LM.Domain.Common;
using LM.Domain.Entities;
using LM.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Test.TransactionTests
{
    public class TransactionServiceFactory
    {
        public Mock<IStoreManager<BookHistory>> BookHistoryRepository = new Mock<IStoreManager<BookHistory>>();
        public Mock<IStoreManager<Book>> BookRepository = new Mock<IStoreManager<Book>>();
        public Mock<IUnitOfWork> UnitOfWork = new Mock<IUnitOfWork>();
        public Mock<IContextAccessor> ContextAccessor = new Mock<IContextAccessor>();
        public Mock<UserManager<LMUser>> UserManager = new Mock<UserManager<LMUser>>();

        public TransactionServiceFactory()
        {
            TransactionsService = new TransactionsService(
                ContextAccessor.Object,
                BookRepository.Object,
                BookHistoryRepository.Object,
                UserManager.Object,
                UnitOfWork.Object
                );

            ContextAccessor.Setup(x => x.GetCurrentUserId()).Returns(new UserPrincipal(TestData.GetAuthenticatedUser()).UserId);
        }

        public TransactionsService TransactionsService { get; set; }
    }
}
