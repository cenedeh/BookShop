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
    public class BookCategoryQuery : IRequest<List<GetBooksDto>>
    {
        public int CategoryId { get; set; }
    }
    public class BookCategoryHandler : IRequestHandler<BookCategoryQuery, List<GetBooksDto>>
    {
        private readonly IBookStoreRepository _bookStoreRepository;

        public BookCategoryHandler(IBookStoreRepository bookStoreRepository)
        {
            _bookStoreRepository = bookStoreRepository;
        }

        public async Task<List<GetBooksDto>> Handle(BookCategoryQuery request, CancellationToken cancellationToken)
        {
            var booksDtos = new List<GetBooksDto>();

            var categories = await _bookStoreRepository.
                FindByCondition<Category>(a => a.Id == request.CategoryId);
               
            //var authors = await 
            //    _bookStoreRepository.FindByCondition<Author>(a =>
            //        a.Books.Any(b => b.Book.Id == categories.Any(b=> b.BookId)));
            foreach (var category in categories)
            {
                var getBooksDto = new GetBooksDto
                {
                    Id = category.Book.Id,
                    Published = category.Book.Published,
                    IsbnCode = category.Book.IsbnCode,
                    Title = category.Book.Title,
                    Category = category.Book.Category.Title,
                    Authors = category.Book.Books.Select(x=> new GetAuthorsDto
                    {
                        Id = x.Author.Id,
                        FirstName = x.Author.FirstName,
                        LastName = x.Author.LastName
                    }).ToList()
                };
                booksDtos.Add(getBooksDto);
            }
                
            return booksDtos;
        }
    }
}
