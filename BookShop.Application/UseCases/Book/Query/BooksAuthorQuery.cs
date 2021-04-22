using BookShop.Application.Interfaces;
using BookShop.Application.UseCases.Book.Dto;
using BookShop.Domain.Model;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
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
            private readonly IMemoryCache _memoryCache;

            public BooksAuthorHandler(IBookStoreRepository bookStoreRepository, IMemoryCache memoryCache)
            {
                _bookStoreRepository = bookStoreRepository;
                _memoryCache = memoryCache;
            }

            public async Task<List<GetBooksDto>> Handle(BooksAuthorQuery request, CancellationToken cancellationToken)
            {
                const string cacheKey = "bookAuthorQueryList";
                if(!_memoryCache.TryGetValue(cacheKey, out List<GetBooksDto> bookQueryDto))
                {
                    bookQueryDto = await GetBooksDtos(request);
                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(2)
                    };
                    _memoryCache.Set(cacheKey, bookQueryDto, cacheExpiryOptions);
                }

                return bookQueryDto;
                    
            }

            private async Task<List<GetBooksDto>> GetBooksDtos(BooksAuthorQuery request)
            {
                var booksDtos = new List<GetBooksDto>();

                var books = await _bookStoreRepository.FindByCondition<AuthorsBooks>(a => a.AuthorId == request.AuthorId);

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
                        Authors = authors.Select(x => new GetAuthorsDto
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
