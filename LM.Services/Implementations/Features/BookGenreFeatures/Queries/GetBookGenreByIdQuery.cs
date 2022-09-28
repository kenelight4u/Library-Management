using LM.Application.Interfaces.Persistence;
using LM.Domain.Common;
using LM.Domain.Entities;
using LM.DTOs.Response.BookGenresVM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LM.Services.Implementations.Features.BookGenreFeatures.Queries
{
    public class GetBookGenreByIdQuery : IRequest<ResultModel<BookGenresVM>>
    {
        public Guid ID { get; set; }

        public class GetBookGenreByIdQueryHandler : IRequestHandler<GetBookGenreByIdQuery, ResultModel<BookGenresVM>>
        {
            private readonly IStoreManager<BookGenres> _bookGenre;

            public GetBookGenreByIdQueryHandler(IStoreManager<BookGenres> bookGenre)
            {
                _bookGenre = bookGenre;
            }

            public async Task<ResultModel<BookGenresVM>> Handle(GetBookGenreByIdQuery query, CancellationToken cancellationToken)
            {
                var bookGenre = await _bookGenre.DataStore.GetAllQuery()
                .Where(x => x.ID == query.ID)
                .Include(x => x.Books)
                .FirstOrDefaultAsync();

                if (bookGenre is null) return new ResultModel<BookGenresVM>($"Book Genre with ID: {query.ID} Not Found");

                return new ResultModel<BookGenresVM> { Data = bookGenre, Message = "Success" };

            }
        }
    }
}
