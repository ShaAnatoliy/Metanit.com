using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Books.Controllers
{
    using Models;

    public class BookController : ApiController
    {
        IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var book = _repository.GetByID(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        public IHttpActionResult Post(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Add(book);
            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }
    }
}
