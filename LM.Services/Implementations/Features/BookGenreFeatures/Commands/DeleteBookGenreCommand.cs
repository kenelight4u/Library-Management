using LM.Application.Interfaces.Persistence;
using LM.Domain.Common;
using LM.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace LM.Services.Implementations.Features.BookGenreFeatures.Commands
{
    public class DeleteBookGenreCommand : IRequest<ResultModel<string>>
    {
        public Guid ID { get; set; }

        public class DeleteBookGenreCommandHandler : IRequestHandler<DeleteBookGenreCommand, ResultModel<string>>
        {
            private readonly IStoreManager<BookGenres> _bookGenre;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteBookGenreCommandHandler(
                IStoreManager<BookGenres> bookGenre,
                IUnitOfWork unitOfWork)
            {
                _bookGenre = bookGenre;
                _unitOfWork = unitOfWork;
            }

            public async Task<ResultModel<string>> Handle(DeleteBookGenreCommand request, CancellationToken cancellationToken)
            {
                var isDeleted = await _bookGenre.DataStore.Delete(request.ID);

                //await _bookGenre.SaveChanges();
                await _unitOfWork.SaveChangesAsync();

                if (isDeleted)
                    return new ResultModel<string> { Data = "Success", Message = $"Book Genre with ID: {request.ID} Deleted Successful" };

                return new ResultModel<string>
                {
                    Data = "Failed",
                    Message = $"Book Genre with ID: {request.ID} Not Found",
                    ValidationErrors = new List<ValidationResult>
                    {new ValidationResult($"Book Genre with ID: {request.ID} Not Found", null)}
                };
            }
        }
    }
}
