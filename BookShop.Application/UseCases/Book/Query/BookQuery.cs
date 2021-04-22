using BookShop.Application.Interfaces;
using BookShop.Application.UseCases.Book.Dto;
using BookShop.Domain.Model;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
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
            private readonly IMemoryCache _memoryCache;

            public BooksHandler(IBookStoreRepository bookStoreRepository, IMemoryCache memoryCache)
            {
                _bookStoreRepository = bookStoreRepository;
                _memoryCache = memoryCache;
            }

            public async Task<GetBooksDto> Handle(BookQuery request, CancellationToken cancellationToken)
            {
                const string cacheKey = "bookQueryList";
                if(!_memoryCache.TryGetValue(cacheKey, out GetBooksDto bookQueryDto))
                {
                    bookQueryDto = await GetBooksDto(request);
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

            private async Task<GetBooksDto> GetBooksDto(BookQuery request)
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
                    Authors = authors.Select(x => new GetAuthorsDto
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
