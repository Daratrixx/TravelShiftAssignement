using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private Repository.BlogRepository _repository;
        public ArticleController(Repository.BlogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Model.Article>>> GetArticles()
        {
            var articles = await _repository.GetArticles();
            return articles.ToArray();
        }
        [HttpGet("Popularity")]
        public async Task<ActionResult<IEnumerable<Model.Article>>> GetArticlesByPopularity()
        {
            var articles = await _repository.GetArticlesQuery();
            return articles.ToArray();
        }
        [HttpGet("Category/{category}")]
        public async Task<ActionResult<IEnumerable<Model.Article>>> GetArticlesForCategory(string category, [FromBody] Repository.BlogRepository.BlogRepositoryRequestParameter parameters)
        {
            var articles = await _repository.GetArticles();
            return articles.ToArray();
        }
        [HttpGet("Category/{category}/Latest")]
        public async Task<ActionResult<IEnumerable<Model.Article>>> GetLatestArticlesForCategory(string category)
        {
            var articles = await _repository.GetArticles();
            return articles.ToArray();
        }
        [HttpGet("Author/{author}")]
        public async Task<ActionResult<IEnumerable<Model.Article>>> GetArticlesForAuthor(string author, [FromBody] Repository.BlogRepository.BlogRepositoryRequestParameter parameters)
        {
            var articles = await _repository.GetArticles();
            return articles.ToArray();
        }
    }
}
