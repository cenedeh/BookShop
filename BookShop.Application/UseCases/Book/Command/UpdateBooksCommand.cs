using BookShop.Application.Interfaces;
using BookShop.Domain.Model;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.UseCases.Book.Command
{
    public class UpdateBooksCommand : IRequest<bool>
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string IsbnCode { get; set; }
        public Category Category { get; set; }
        public DateTime DatePublished { get; set; }

        public class UpdateBooksCommandHandler : IRequestHandler<UpdateBooksCommand, bool>
        {
            private readonly IBookStoreRepository _bookStoreRepository;
            public UpdateBooksCommandHandler(IBookStoreRepository bookStoreRepository)
            {
                _bookStoreRepository = bookStoreRepository;
            }
            
            public async Task<bool> Handle(UpdateBooksCommand request, CancellationToken cancellationToken)
            {
                var book = await _bookStoreRepository.FindById<Domain.Model.Book>(request.BookId);
                if (book == null) return false;
                book.Title = request.Title;
                book.IsbnCode = request.IsbnCode;
                book.Published = request.DatePublished;
                book.Category = request.Category;
                _bookStoreRepository.Update(book);
                var result = await _bookStoreRepository.SaveChanges();
                return result;
            }
        }
    }
}
