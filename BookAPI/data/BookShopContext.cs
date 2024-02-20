using BookAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.data
{
    public class BookShopContext : DbContext
    {
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB;Initial Catalog=BookShopDB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var authors = new Author[]
            {
                new Author { Id = 1, FirstName = "Jeff", LastName = "Olsen" },
                new Author { Id = 2, FirstName = "Charles", LastName="Duhigg"},
                new Author { Id = 3, FirstName = "Victor", LastName="Hugo"},
                new Author { Id = 4, FirstName = "Emile", LastName="Zola"},
                new Author { Id = 5, FirstName = "Pablo", LastName="Coelho"},
            };

            modelBuilder.Entity<Author>().HasData(authors);

            var books = new Book[]
            {
                new Book{ Id = 1, AuthorId = 1, Title = "The slight Edge", PublishDate = new DateOnly(1990, 1, 1), Price = 17.50m },
                new Book{ Id = 2, AuthorId = 5, Title = "L'alchimiste", PublishDate = new DateOnly(1988, 1, 1), Price = 22.50m },
                new Book{ Id = 3, AuthorId = 5, Title = "Onze minutes", PublishDate = new DateOnly(2003, 1, 1), Price = 19.50m }
            };
            modelBuilder.Entity<Book>().HasData(books);


        }
    }
}
