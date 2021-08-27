using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArticleService.Repository
{
    public class BlogRepository : RestClient
    {
        private static readonly string ServiceBaseUrl = "http://localhost:8081/";
        private static readonly string AuthorApiRoute = "api/Authors/";
        private static readonly string ArticleApiRoute = "api/Articles/";

        private IEnumerable<Model.Author> authorCache = null;
        private IEnumerable<Model.Article> articleCache = null;

        public BlogRepository() : base(ServiceBaseUrl)
        {
            this.AddDefaultHeader("Content-Type", "application/json");
            this.AddDefaultHeader("Accept", "*/*");
            this.AddDefaultHeader("Authorization", "2a85c8bc-8112-485d-acb5-1c93db3c4d82");
        }

        #region AUTHORS
        public async Task<IEnumerable<Model.Author>> GetAuthors()
        {
            if (authorCache == null)
            {
                authorCache = await Get<Model.Author[]>(AuthorApiRoute);
            }
            return authorCache;
        }
        public async Task<Model.Author> GetAuthorById(long id)
        {
            return (await GetAuthors()).Single(x => x.id == id);
            //return await Get<Model.Author>(AuthorApiRoute + "ById/" + id);
        }
        public async Task<Model.Author> GetAuthorByName(string name)
        {
            return (await GetAuthors()).Single(x => x.fullName == name);
            //return await Get<Model.Author>(AuthorApiRoute + "ByName/" + name);
        }
        public async Task<bool> UpdateAuthor(long id, Model.Author author)
        {
            return await Put<Model.Author>(AuthorApiRoute + id, author);
        }

        public async Task BindAuthorToArticle(Model.Article article)
        {
            var author = (await GetAuthorById(article.idAuthor));
            article.author = author;
        }
        #endregion

        #region ARTICLES
        public async Task<IEnumerable<Model.Article>> GetArticles()
        {
            if (articleCache == null)
            {
                articleCache = await Get<Model.Article[]>(ArticleApiRoute);
                foreach (var article in articleCache) await BindAuthorToArticle(article);
            }
            return articleCache;
        }
        public async Task<IEnumerable<Model.Article>> GetArticlesQuery(int page = 0, int count = 5, string filterField = null, string filterValue = null, string orderField = "viewCount", bool orderAscending = false)
        {
            var articles = await Get<Model.Article[]>(ArticleApiRoute + "Query", new { filterField, filterValue, page, count, orderField, orderAscending });
            foreach (var article in articles) await BindAuthorToArticle(article);
            return articles;
        }

        public async Task<IEnumerable<Model.Article>> GetArticlesForAuthorId(long id, BlogRepositoryRequestParameter parameters)
        {
            var articles = await Get<Model.Article[]>(ArticleApiRoute + "Query", new { filterField = "idAuthor", filterValue = id, parameters.page, parameters.count, parameters.orderField, parameters.orderAscending });
            foreach (var article in articles) await BindAuthorToArticle(article);
            return articles;
        }

        public async Task<IEnumerable<Model.Article>> GetArticlesForAuthorName(string name, BlogRepositoryRequestParameter parameters)
        {
            var author = await GetAuthorByName(name);
            var articles = await Get<Model.Article[]>(ArticleApiRoute + "Query", new { filterField = "idAuthor", filterValue = author.id, parameters.page, parameters.count, parameters.orderField, parameters.orderAscending });
            foreach (var article in articles) article.author = author;
            return articles;
        }

        public async Task<IEnumerable<Model.Article>> GetArticlesForCategory(string category, BlogRepositoryRequestParameter parameters)
        {
            var articles = await Get<Model.Article[]>(ArticleApiRoute + "Query", new { filterField = "category", filterValue = category, parameters.page, parameters.count, parameters.orderField, parameters.orderAscending });
            foreach (var article in articles) await BindAuthorToArticle(article);
            return articles;
        }

        public async Task<Model.Article> GetLatestArticlesForCategory(string category)
        {
            var articles = await Get<Model.Article[]>(ArticleApiRoute + "Query", new { filterField = "category", filterValue = category, page = 0, count = 1, orderField = "dateCreated", orderAscending = false });
            var article = articles.First();
            await BindAuthorToArticle(article);
            return article;
        }
        #endregion

        #region REST METHODS
        public async Task<T> Get<T>(string endpoint, object body = null)
        {
            var request = new RestRequest(endpoint);
            request.Method = Method.GET;
            if (body != null)
            {
                request.AddJsonBody(body);
            }
            var response = await this.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<T>(response.Content);
            return responseData;
        }
        public async Task<bool> Put<T>(string endpoint, object body = null)
        {
            var request = new RestRequest(endpoint);
            request.Method = Method.PUT;
            if (body != null)
            {
                request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            }
            var response = await this.ExecuteAsync(request);
            return response.IsSuccessful;
        }
        public async Task<T> Post<T>(string endpoint, object body = null)
        {
            var request = new RestRequest(endpoint);
            request.Method = Method.POST;
            if (body != null)
            {
                request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            }
            var response = await this.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<T>(response.Content);
            return responseData;
        }
        public async Task<bool> Delete(string endpoint, object body = null)
        {
            var request = new RestRequest(endpoint);
            request.Method = Method.DELETE;
            if (body != null)
            {
                request.AddJsonBody(JsonConvert.SerializeObject(body));
            }
            var response = await this.ExecuteAsync(request);
            return response.IsSuccessful;
        }
        #endregion

        public class BlogRepositoryRequestParameter
        {
            public string orderField { get; set; } = "viewCount";
            public bool orderAscending { get; set; } = false;
            public int page { get; set; } = 0;
            public int count { get; set; } = 5;
        }
    }
}
