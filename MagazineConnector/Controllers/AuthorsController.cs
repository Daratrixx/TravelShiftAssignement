using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagazineConnector.Contexts;
using MagazineConnector.Model;

namespace MagazineConnector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly Repositories.AuthorRepository _repository;

        public AuthorsController(Repositories.AuthorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await Task.Run(() => _repository.GetAuthors().ToArray());
            return authors;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<Author>> GetAuthor(long id)
        {
            var author = await Task.Run(() => _repository.GetAuthorById(id));

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // GET: api/Authors/5
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<Author>> GetAuthorByName(string name)
        {
            var author = await Task.Run(() => _repository.GetAuthorByName(name));

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(long id, Author author)
        {
            if (id != author.id)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _repository.UpdateAuthor(author));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            author = await Task.Run(() => _repository.CreateAuthor(author));

            return CreatedAtAction("GetAuthor", new { id = author.id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(long id)
        {
            var author = await Task.Run(() => _repository.GetAuthorById(id));
            if (author == null)
            {
                return NotFound();
            }

            await Task.Run(() => _repository.DeleteAuthor(id));

            return Ok();
        }

        private bool AuthorExists(long id)
        {
            return _repository.AuthorExists(id);
        }
        private bool AuthorExists(string name)
        {
            return _repository.AuthorExists(name);
        }
    }
}
