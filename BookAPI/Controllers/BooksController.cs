using BookAPI.data;
using BookAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetBooks()
        {
            return new JsonResult(BooksData.current.Books);
        }

        [HttpGet("{id}")]
        public JsonResult GetBook(int id) {
            var book = BooksData.current.Books.FirstOrDefault(b => b.Id == id);
            return new JsonResult(book);
        }

        [HttpPost]
        public JsonResult AddBook(BookDto book)
        {
            BooksData.current.Books.Add(book);
            return new JsonResult(book);
        }

        [HttpPut("{id}")] 
        public JsonResult EditBook(BookDto book)
        {
            //
        }

        [HttpDelete("{id}")]
        public string DeleteBook(int id) {
            //
        }

    }
}
