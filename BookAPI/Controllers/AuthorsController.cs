using BookAPI.data;
using BookAPI.Entities;
using BookAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var authors = _context.Authors.Include(a=>a.Books);
            var authorsToReturn = new List<AuthorDto>();
            foreach (var author in authors)
            {
                var auth = new AuthorDto() { Id = author.Id, FirstName = author.FirstName, LastName = author.LastName };
                foreach (var book in author.Books)
                {
                    auth.Books.Add(new BookDto() { Id = book.Id, Title = book.Title, Price = book.Price, PublishDate = book.PublishDate });
                }
                authorsToReturn.Add(auth);
            }
            return Ok(authorsToReturn);
        }

        [HttpGet("{id}")]
        public ActionResult<AuthorDto> GetAuthorsById(int id)
        {
            var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            //var author = _context.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            var authorToReturn = new AuthorDto() { Id = author.Id, FirstName = author.FirstName, LastName = author.LastName };
            foreach (var book in author.Books)
            {
                authorToReturn.Books.Add(new BookDto() { Id = book.Id, Title = book.Title, Price = book.Price, PublishDate= book.PublishDate });
            }
           return Ok(authorToReturn);
        }

        [HttpPost]
        public ActionResult<AuthorDto> AddAuthor(AuthorDto author)
        {
            var authorToAdd = new Author() { FirstName = author.FirstName, LastName = author.LastName };
            foreach (var book in author.Books)
            {
                authorToAdd.Books.Add(new Book() { Title = book.Title, Price = book.Price, PublishDate = book.PublishDate });
            }

            _context.Authors.Add(authorToAdd);
            _context.SaveChanges();
            return Ok(author);
        }

        [HttpPut("{id}")]
        public ActionResult<AuthorDto> EditAuthor(int id, AuthorDto author)
        {
            var authorToEdit = _context.Authors.Find(id);

            if (authorToEdit == null)
            {
                return NotFound();
            }
            authorToEdit.FirstName = author.FirstName;
            authorToEdit.LastName = author.LastName;
            authorToEdit.Books = new List<Book>();
            foreach (var book in author.Books)
            {
                authorToEdit.Books.Add(new Book() { Title = book.Title, Price = book.Price, PublishDate = book.PublishDate });
            }
            _context.SaveChanges();
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteAuthor(int id)
        {
            var authorToDelete = _context.Authors.Find(id);
            if (authorToDelete == null)
            {
                return NotFound();
            }
            _context.Authors.Remove(authorToDelete);
            _context.SaveChanges();
            return Ok("Auteur supprimé");
        }
    }
}
