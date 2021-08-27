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
    public class ArticlesController : ControllerBase
    {
        private readonly Repositories.ArticleRepository _repository;

        public ArticlesController(Repositories.ArticleRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            var articles = await Task.Run(() => _repository.GetArticles().ToArray());
            return articles;
        }

        // GET: api/Articles2
        [HttpGet("Query")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesQuery([FromBody] Repositories.ArticleRepository.ArticleRepositoryRequestParameter parameters)
        {
            var articles = await Task.Run(()=> _repository.GetArticles(parameters).ToArray());
            return articles;
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(long id)
        {
            var article = await Task.Run(() => _repository.GetArticle(id));

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(long id, Article article)
        {
            if (id != article.id)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _repository.UpdateArticle(article));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            article = await Task.Run(() => _repository.CreateArticle(article)); 

            return CreatedAtAction("GetArticle", new { id = article.id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(long id)
        {
            if (!ArticleExists(id))
            {
                return NotFound();
            }
            await Task.Run(() => _repository.DeleteArticle(id));

            return Ok();
        }

        private bool ArticleExists(long id)
        {
            return _repository.ArticleExists(id);
        }
    }
}
