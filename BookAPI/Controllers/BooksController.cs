using BookAPI.data;
using BookAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/Authors/{authorId}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooksByAuthor(int authorId)
        {
            var author = BooksData.current.Authors.FirstOrDefault(a=>a.Id==authorId);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author.Books);
        }

        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBookById(int authorId, int id) {
            var author = BooksData.current.Authors.FirstOrDefault(a => a.Id == authorId);
            if(author == null)
            {
                return NotFound();
            }

            var book = author.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<BookDto> AddBook(int authorId, BookDto book)
        {
            var author = BooksData.current.Authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                return NotFound();
            }

            author.Books.Add(book);
            return Ok(book);
        }

        [HttpPut("{id}")] 
        public ActionResult<BookDto> EditBook(int authorId, int id, BookDto book)
        {

            var author = BooksData.current.Authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                return NotFound();
            }

            var index = author.Books.FindIndex(b=> b.Id == id);
            if(index == -1)
            {
                return NotFound();
            }
            author.Books[index] = book;
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteBook(int authorId, int id) {
            var author = BooksData.current.Authors.FirstOrDefault(a => a.Id == authorId);
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
            return Ok("Livre supprimé");
        }

    }
}
