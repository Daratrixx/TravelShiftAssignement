using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Repositories
{
    public class ArticleRepository
    {
        public IEnumerable<Model.Article> GetArticles()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Model.Article> GetArticles(ArticleRepositoryRequestParameter parameters)
        {
            throw new NotImplementedException();
        }
        public Model.Article GetArticle(long id)
        {
            throw new NotImplementedException();
        }

        public class ArticleRepositoryRequestParameter
        {
            public string filterField;
            public string filterValue;
            public string orderField;
            public bool orderAscending;
            public int page = 0;
            public int count = 10;
        }
    }
}
