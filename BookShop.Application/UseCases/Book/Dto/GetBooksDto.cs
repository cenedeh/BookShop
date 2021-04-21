using BookShop.Domain.Model;
using System;

namespace BookShop.Application.UseCases.Book.Dto
{
    public class GetBooksDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IsbnCode { get; set; }
        public DateTime Published { get; set; }
        public Category Category { get; set; }
    }
}
