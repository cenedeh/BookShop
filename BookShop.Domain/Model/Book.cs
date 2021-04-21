using System;
using System.Collections.Generic;

namespace BookShop.Domain.Model
{
    public class Book
    {
        public Book(){ }

        private Book(string title, string isbnCode, int categoryId){
            Title = title;
            IsbnCode = isbnCode;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string IsbnCode { get; set; }
        public DateTime Published { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<AuthorsBooks> Books { get; set; } = new List<AuthorsBooks>();
        
        public class Factory
        {
            public static Book Instance(string title, string isbnCode, int categoryId) => 
                new Book(title, isbnCode, categoryId);
        }
    }
}
