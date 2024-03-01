using BookAPI.data;
using BookAPI.Entities;
using BookAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private BookShopContext _context;

        public BooksController(BookShopContext context)
        {
            _context = context;
        }
        // GET: api/<BooksController>
        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooks()
        {
            var books = _context.Books;
            return Ok(books);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBookById(int id)
        {
            var book = _context.Books.Find(id);
            return Ok(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        public ActionResult<BookDto> AddBook(BookDto book)
        {
            var newBook = new Book() { Title = book.Title, Price = book.Price, PublishDate = book.PublishDate, AuthorId = book.AuthorId };
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok(book);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public ActionResult<BookDto> EditBook(int id, BookDto book)
        {
            _context.Books.Update(new Book() { Id = id, Title = book.Title, Price = book.Price, PublishDate = book.PublishDate, AuthorId = book.AuthorId });
            _context.SaveChanges();
            return Ok(book);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Books.Remove(_context.Books.Find(id));
            _context.SaveChanges();
        }
    }
}
