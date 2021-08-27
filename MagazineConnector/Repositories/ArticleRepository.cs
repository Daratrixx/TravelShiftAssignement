using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Repositories
{
    public class ArticleRepository
    {
        private Contexts.BlogContext _context;
        public ArticleRepository(Contexts.BlogContext context)
        {
            _context = context;
        }
        public IEnumerable<Model.Article> GetArticles()
        {
            return _context.Articles;
        }
        public IEnumerable<Model.Article> GetArticles(ArticleRepositoryRequestParameter parameters)
        {
            IQueryable<Model.Article> output = _context.Articles;

            if (!string.IsNullOrWhiteSpace(parameters.filterField) && !string.IsNullOrWhiteSpace(parameters.filterValue))
            {
                var filterFieldProperty = typeof(Model.Article).GetProperty(parameters.filterField);
                output = output.Where(x => filterFieldProperty.GetValue(x).ToString() == parameters.filterValue);
            }

            if (!string.IsNullOrWhiteSpace(parameters.orderField))
            {
                var orderFieldProperty = typeof(Model.Article).GetProperty(parameters.orderField);
                if (parameters.orderAscending)
                    output = output.OrderBy(x => orderFieldProperty.GetValue(x).ToString());
                else
                    output = output.OrderByDescending(x => orderFieldProperty.GetValue(x).ToString());
            }

            output = output.Skip(parameters.page * parameters.count).Take(parameters.count);
            return output;
        }
        public Model.Article GetArticle(long id)
        {
            return _context.Articles.Where(x => x.id == id).FirstOrDefault();
        }
        public Model.Article CreateArticle(Model.Article article)
        {
            _context.Add(article);
            _context.SaveChanges();
            return article;
        }
        public void UpdateArticle(Model.Article article)
        {
            _context.Update(article);
            _context.SaveChanges();
        }
        public void DeleteArticle(long id)
        {
            _context.Remove(_context.Articles.First(x => x.id == id));
            _context.SaveChanges();
        }

        public bool ArticleExists(long id)
        {
            return _context.Articles.Any(e => e.id == id);
        }

        public class ArticleRepositoryRequestParameter
        {
            public string filterField { get; set; } = null;
            public string filterValue { get; set; } = null;
            public string orderField { get; set; } = null;
            public bool orderAscending { get; set; } = false;
            public int page { get; set; } = 0;
            public int count { get; set; } = 10;
        }
    }
}
