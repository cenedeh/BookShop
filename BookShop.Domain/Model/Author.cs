using System.Collections.Generic;

namespace BookShop.Domain.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AuthorsBooks> Books { get; set; } = new List<AuthorsBooks>();
    }
}
