using BookShop.Application.Interfaces;
using BookShop.Application.UseCases.Book.Dto;
using BookShop.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
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
                    FindByCondition<AuthorsBooks>(a => a.AuthorId == request.AuthorId);
               
                var authors = await 
                    _bookStoreRepository.FindByCondition<Author>(a =>
                        a.Books.Any(b => b.BookId == request.AuthorId));
                foreach (var book in books)
                {
                    var getBooksDto = new GetBooksDto
                    {
                        Id = book.Book.Id,
                        Published = book.Book.Published,
                        IsbnCode = book.Book.IsbnCode,
                        Title = book.Book.Title,
                        Category = book.Book.Category.Title,
                        Authors = authors.Select(x=> new GetAuthorsDto
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName
                        }).ToList()
                    };
                    booksDtos.Add(getBooksDto);
                }
                
                return booksDtos;
            }
        }
    }
}
