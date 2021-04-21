using BookShop.Application.Interfaces;
using BookShop.Application.UseCases.Book.Dto;
using BookShop.Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.UseCases.Book.Query
{
    public class BookQuery : IRequest<GetBooksDto>
    {
        public int BookId { get; set; }

        public class BooksHandler : IRequestHandler<BookQuery, GetBooksDto>
        {
            private readonly IBookStoreRepository _bookStoreRepository;
            public BooksHandler(IBookStoreRepository bookStoreRepository)
            {
                _bookStoreRepository = bookStoreRepository;
            }

            public async Task<GetBooksDto> Handle(BookQuery request, CancellationToken cancellationToken)
            {
                var book = await _bookStoreRepository.FindById<Domain.Model.Book>(request.BookId);
                var authors = await 
                    _bookStoreRepository.FindByCondition<Author>(a =>
                        a.Books.Any(b => b.BookId == request.BookId));
                var getBooksDto = new GetBooksDto
                {
                    Id = book.Id,
                    Published = book.Published,
                    IsbnCode = book.IsbnCode,
                    Title = book.Title,
                    Category = book.Category.Title,
                    Authors = authors.Select(x=> new GetAuthorsDto
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    }).ToList()
                    
                };
                return getBooksDto;
            }
        }
    }
}
