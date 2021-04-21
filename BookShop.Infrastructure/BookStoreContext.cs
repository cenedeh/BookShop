using BookShop.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookShop.Infrastructure
{
    public class BookStoreContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> builder):base(builder) { }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuthorsBooks>().HasKey(k => new { k.AuthorId, k.BookId });
            builder.Entity<AuthorsBooks>().HasOne(x => x.Book).WithMany(x => x.Books).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AuthorsBooks_Book");
            builder.Entity<AuthorsBooks>().HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AuthorsBooks_Author");
            base.OnModelCreating(builder);

        }
    }
}
