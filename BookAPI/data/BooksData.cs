using BookAPI.Model;

namespace BookAPI.data
{
    public class BooksData{
        public List<BookDto> Books { get; set; }
        public static BooksData current = new BooksData();
        public BooksData()
        {
            Books = new List<BookDto>() {
                new BookDto() { 
                    Id = 1, 
                    Title = "The slight Edge", 
                    Author = "Jeff Olsen", 
                    PublishDate = new DateOnly(2013, 11, 04), 
                    Price = 20.50m,
                },
                new BookDto() {
                    Id = 2,
                    Title = "Power Of Habits",
                    Author = "Charles Duhigg",
                    PublishDate = new DateOnly(2012, 02, 28),
                    Price = 20.50m,
                },
                new BookDto() {
                    Id = 3,
                    Title = "Atmic Habits",
                    Author = "James Clear",
                    PublishDate = new DateOnly(2018, 10, 16),
                    Price = 20.50m,
                }
            };
        }
    }
}
