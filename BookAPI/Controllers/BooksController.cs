using BookAPI.data;
using BookAPI.Entities;
using BookAPI.Model;
using BookAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Controllers
{
    [Route("api/Authors/{authorId}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookShopContext _context;
        private readonly MyService _myService;

        public BooksController(BookShopContext context, MyService myService) {
            _context = context;
            _myService = myService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooksByAuthor(int authorId)
        {
            _myService.showRequestDetails("GET", DateTime.Now);
            var author = _context.Authors.Include(a=>a.Books).FirstOrDefault(a=>a.Id==authorId);
            if (author == null)
            {
                return NotFound();
            }
            
            var books = new List<BookDto>();
            foreach (var b in author.Books)
            {
                var book = new BookDto() { Id = b.Id, Title = b.Title, Price = b.Price, PublishDate = b.PublishDate };
                books.Add(book);
            }

            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBookById(int authorId, int id) {
            _myService.showRequestDetails("GET", DateTime.Now);
            var author = _context.Authors.Include(a=>a.Books).FirstOrDefault(a => a.Id == authorId);
            if(author == null)
            {
                return NotFound();
            }

            var book = author.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(new BookDto { Id = book.Id, Title = book.Title, Price = book.Price, PublishDate = book.PublishDate});
        }

        [HttpPost]
        public ActionResult<BookDto> AddBook(int authorId, BookDto book)
        {
            _myService.showRequestDetails("POST", DateTime.Now); 
            var author = _context.Authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                return NotFound();
            }

            var newBook = new Book() { Title = book.Title, Price = book.Price, PublishDate= book.PublishDate, AuthorId = authorId };
            author.Books.Add(newBook);
            _context.SaveChanges();
            return Ok(book);
        }

        [HttpPut("{id}")] 
        public ActionResult<BookDto> EditBook(int authorId, int id, BookDto book)
        {

            _myService.showRequestDetails("PUT", DateTime.Now); 
            var author = _context.Authors.Include(a=>a.Books).FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                return NotFound();
            }

            var index = author.Books.FindIndex(b=> b.Id == id);
            if(index == -1)
            {
                return NotFound();
            }
            author.Books[index] = new Book() { Title = book.Title, Price = book.Price, PublishDate = book.PublishDate };
            _context.SaveChanges();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteBook(int authorId, int id) {
            _myService.showRequestDetails("DELETE", DateTime.Now); 
            var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                return NotFound();
            }

            var index = author.Books.FindIndex(b => b.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            author.Books.RemoveAt(index);
            _context.SaveChanges();
            return Ok("Livre supprimé");
        }

    }
}
