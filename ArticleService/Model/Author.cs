using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArticleService.Model
{
    public class Author
    {
        public long id { get; set; }
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string fullName { get; set; }
    }
}
