using BookShop.Application.Interfaces;
using BookShop.Application.UseCases.Book.Dto;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.UseCases.Book.Query
{
    public class BooksAuthorQuery : IRequest<List<GetBooksDto>>
    {
        public int AuthorId { get; set; }

        public class BooksAuthorHandler : IRequestHandler<BooksAuthorQuery, List<GetBooksDto>>
        {
            private readonly IBookStoreRepository _bookStoreRepository;

            public BooksAuthorHandler(IBookStoreRepository bookStoreRepository)
            {
                _bookStoreRepository = bookStoreRepository;
            }

            public async Task<List<GetBooksDto>> Handle(BooksAuthorQuery request, CancellationToken cancellationToken)
            {
                var booksDtos = new List<GetBooksDto>();
                var books = await _bookStoreRepository.
                    FindByCondition<Domain.Model.AuthorsBooks>(a => a.AuthorId == request.AuthorId);
                foreach (var book in books)
                {
                    var getBooksDto = new GetBooksDto
                    {
                        Id = book.Book.Id,
                        Published = book.Book.Published,
                        IsbnCode = book.Book.IsbnCode,
                        Title = book.Book.Title,
                        Category = book.Book.Category.Title
                    };
                    booksDtos.Add(getBooksDto);
                }
                
                return booksDtos;
            }
        }
    }
}
