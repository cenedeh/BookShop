using BookShop.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.UseCases.Book.Command
{
    public class DeleteBooksCommand : IRequest<bool>
    {
        public int BookId { get; set; }

        public class DeleteBooksCommandHandler : IRequestHandler<DeleteBooksCommand, bool>
        {
            private readonly IBookStoreRepository _bookStoreRepository;
            public DeleteBooksCommandHandler(IBookStoreRepository bookStoreRepository)
            {
                _bookStoreRepository = bookStoreRepository;
            }
            
            public async Task<bool> Handle(DeleteBooksCommand request, CancellationToken cancellationToken)
            {
                var book = await _bookStoreRepository.FindById<Domain.Model.Book>(request.BookId);
                if (book == null) return false;
                _bookStoreRepository.DeleteAsync(book);
                var result = await _bookStoreRepository.SaveChanges();
                return result;
            }
        }
    }
}
