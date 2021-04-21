using BookShop.Application.Exceptions;
using BookShop.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.UseCases.Book.Command
{
    public class AddBooksCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string IsbnCode { get; set; }
        public int CategoryId { get; set; }

        public class BooksCommandHandler : IRequestHandler<AddBooksCommand, bool>
        {
            private readonly IBookStoreRepository _bookStoreRepository;
            public BooksCommandHandler(IBookStoreRepository bookStoreRepository)
            {
                _bookStoreRepository = bookStoreRepository;
            }
            
            public async Task<bool> Handle(AddBooksCommand request, CancellationToken cancellationToken)
            {
                var doesBookExist = await _bookStoreRepository.
                    FindSingleByCondition<Domain.Model.Book>(x=>x.IsbnCode == request.IsbnCode);
                if (doesBookExist != null) throw new DuplicateExceptionException("Duplicate entity found");
                var book = Domain.Model.Book.Factory.Instance(request.Title, request.IsbnCode, request.CategoryId);
                _bookStoreRepository.Create(book);
                var result = await _bookStoreRepository.SaveChanges();
                return result;
            }
        }
    }
}
