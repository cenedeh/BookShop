﻿using BookShop.Application.Interfaces;
using BookShop.Application.UseCases.Book.Dto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.UseCases.Book.Query
{
    public class BookQuery : IRequest<GetBooksDto>
    {
        public int AuthorId { get; set; }

        public class BooksHandler : IRequestHandler<BookQuery, GetBooksDto>
        {
            private readonly IBookStoreRepository _bookStoreRepository;
            public BooksHandler(IBookStoreRepository bookStoreRepository)
            {
                _bookStoreRepository = bookStoreRepository;
            }

            public async Task<GetBooksDto> Handle(BookQuery request, CancellationToken cancellationToken)
            {
                var book = await _bookStoreRepository.FindById<Domain.Model.Book>(request.AuthorId); 
                var getBooksDto = new GetBooksDto
                {
                    Id = book.Id,
                    Published = book.Published,
                    IsbnCode = book.IsbnCode,
                    Title = book.IsbnCode,
                    Category = book.Category
                };
                return getBooksDto;
            }
        }
    }
}
