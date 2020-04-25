

using Microsoft.AspNetCore.Mvc;
using MongoDbDAL;
using MongoDbDAL.Entities;
using System.Collections.Generic;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly EmailService _emailService;

        public BooksController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult<List<Email>> Get() =>
            _emailService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Email> Get(string id)
        {
            var book = _emailService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Email> Create(Email book)
        {
            _emailService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Email bookIn)
        {
            var book = _emailService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _emailService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _emailService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _emailService.Remove(book.Id);

            return NoContent();
        }
    }
}