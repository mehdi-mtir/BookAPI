using BookAPI.data;
using BookAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private BookShopContext _context;
        public AuthorsController(BookShopContext context) {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            return Ok(_context.Authors);
        }

        [HttpGet("{id}")]
        public ActionResult<AuthorDto> GetAuthorsById(int id)
        {
            var author = BooksData.current.Authors.FirstOrDefault(b => b.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<AuthorDto> AddAuthor(AuthorDto author)
        {
            BooksData.current.Authors.Add(author);
            return Ok(author);
        }

        [HttpPut("{id}")]
        public ActionResult<AuthorDto> EditAuthor(int id, AuthorDto author)
        {
            var index = BooksData.current.Authors.FindIndex(b => b.Id == id);

            if (index == -1)
            {
                return NotFound();
            }
            BooksData.current.Authors[index] = author;
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteAuthor(int id)
        {
            var index = BooksData.current.Authors.FindIndex(b => b.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            BooksData.current.Authors.RemoveAt(index);
            return Ok("Auteur supprimé");
        }
    }
}
