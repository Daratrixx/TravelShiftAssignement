using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Model
{
    public class Article
    {
        public long id { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime dateCreated { get; set; }
        public long viewCount { get; set; }
    }
}
