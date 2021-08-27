using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleService.Model
{
    public class Article
    {
        public long id { get; set; }
        public long idAuthor { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime dateCreated { get; set; }
        public long viewCount { get; set; }

        public Author author { get; set; }
    }
}
