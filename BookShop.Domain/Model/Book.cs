using System;
using System.Collections.Generic;

namespace BookShop.Domain.Model
{
    public class Book
    {
        public Book(){ }

        private Book(string title, string isbnCode, Category category, DateTime publishedDate){
            Title = title;
            IsbnCode = isbnCode;
            Category = category;
            Published = publishedDate;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string IsbnCode { get; set; }
        public DateTime Published { get; set; }
        public Category Category { get; set; }
        public ICollection<AuthorsBooks> Books { get; set; } = new List<AuthorsBooks>();
        
        public class Factory
        {
            public static Book Instance(string title, string isbnCode, Category category, DateTime publishedDate) => 
                new Book(title, isbnCode, category, publishedDate);
        }
    }
}
