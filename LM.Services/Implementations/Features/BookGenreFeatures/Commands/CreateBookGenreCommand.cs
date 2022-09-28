using LM.Application.Interfaces.Persistence;
using LM.Application.Interfaces.Utilities;
using LM.Domain.Common;
using LM.Domain.Entities;
using LM.DTOs.Request.BookGenreDTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LM.Services.Implementations.Features.BookGenreFeatures.Commands
{
    public class CreateBookGenreCommandHandler : IRequestHandler<BookGenresDTO, ResultModel<string>>
    {
        private readonly IContextAccessor _contextAccessor;
        private readonly IStoreManager<BookGenres> _bookGenre;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookGenreCommandHandler(
            IStoreManager<BookGenres> bookGenre,
            IContextAccessor contextAccessor,
            IUnitOfWork unitOfWork)
        {
            _bookGenre = bookGenre;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<string>> Handle(BookGenresDTO request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var bookGenre = await _bookGenre.DataStore.GetAllQuery().Where(x => x.Name == request.Name).FirstOrDefaultAsync();

            if (bookGenre is not null)
                return new ResultModel<string>
                {
                    Data = "Failed",
                    Message = $"Book Genre with Name: {bookGenre.Name} Exist",
                    ValidationErrors = new List<ValidationResult>
                    {new ValidationResult($"Book Genre with Name: {bookGenre.Name} Exist", null)}
                };

            var newBookGenre = new BookGenres()
            {
                Name = request.Name,
                CreationTime = DateTime.Now,
                Description = request.Description,
                CreatorUserId = userId.ToString(),
                ID = Guid.NewGuid()
            };

            await _bookGenre.DataStore.Add(newBookGenre);
            await _unitOfWork.SaveChangesAsync();

            return new ResultModel<string> { Data = "Success", Message = "Book Genre Created Successfully" };
        }
    }
}
