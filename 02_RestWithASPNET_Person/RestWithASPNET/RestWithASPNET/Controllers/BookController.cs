using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Business;
using System.Collections.Generic;
using RestWithASPNET.Data.DTO;
using Microsoft.AspNetCore.Authorization;

namespace RestWithASPNET.Controllers
{

    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;

        // Declaration of the service used
        private IBookBusiness _bookBusiness;

        // Injection of an instance of IBookService
        // when creating an instance of BookController
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        // Maps GET requests to https://localhost:{port}/api/book
        // Get no parameters for FindAll -> Search All
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookDTO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        // Maps GET requests to https://localhost:{port}/api/book/{id}
        // receiving an ID as in the Request Path
        // Get with parameters for FindById -> Search by ID
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookDTO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindByID(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // Maps POST requests to https://localhost:{port}/api/book/
        // [FromBody] consumes the JSON object sent in the request body
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] BookDTO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        // Maps PUT requests to https://localhost:{port}/api/book/
        // [FromBody] consumes the JSON object sent in the request body
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] BookDTO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        // Maps DELETE requests to https://localhost:{port}/api/book/{id}
        // receiving an ID as in the Request Path
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}