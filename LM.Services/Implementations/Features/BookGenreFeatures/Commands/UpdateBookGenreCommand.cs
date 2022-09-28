using LM.Application.Interfaces.Persistence;
using LM.Domain.Common;
using LM.Domain.Entities;
using LM.DTOs.Request.BookGenreDTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LM.Services.Implementations.Features.BookGenreFeatures.Commands
{
    public class UpdateBookGenreCommandHandler : IRequestHandler<EditBookGenresDTO, ResultModel<string>>
    {
        private readonly IStoreManager<BookGenres> _bookGenre;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookGenreCommandHandler(
            IStoreManager<BookGenres> bookGenre,
            IUnitOfWork unitOfWork)
        {
            _bookGenre = bookGenre;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<string>> Handle(EditBookGenresDTO request, CancellationToken cancellationToken)
        {
            var bookGenre = await _bookGenre.DataStore.GetAllQuery().Where(x => x.ID == request.ID).FirstOrDefaultAsync();

            if (bookGenre is null)
                return new ResultModel<string>
                {
                    Data = "Failed",
                    Message = $"Book Genre with Name: {request.Name} does not Exist",
                    ValidationErrors = new List<ValidationResult>
                    {new ValidationResult($"Book Genre with Name: {request.Name} does not Exist", null)}
                };

            bookGenre.LastModificationTime = DateTime.Now;
            bookGenre.Name = request.Name is null ? bookGenre.Name : request.Name;
            bookGenre.Description = request.Description is null ? bookGenre.Description : request.Description;

            _bookGenre.DataStore.Update(bookGenre);
            //await _bookGenre.SaveChanges();
            await _unitOfWork.SaveChangesAsync();

            return new ResultModel<string> { Data = "Success", Message = "Book Genre Updated Successfully" };

        }
        
    }
}
