using System;
using System.Collections.Generic;

namespace BookShop.Application.UseCases.Book.Dto
{
    public class GetBooksDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IsbnCode { get; set; }
        public DateTime Published { get; set; }
        public string Category { get; set; }
        public List<GetAuthorsDto> Authors { get; set; }
    }
}
