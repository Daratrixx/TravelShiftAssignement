using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Contexts.Seeds
{
    public class ArticleSeed
    {
        private readonly BlogContext _context;

        public ArticleSeed(BlogContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            _nextId = 1;
            CreateAndAddNewArticle(1, "Sport", "The guy did the thing", SeedTexts.LoremIpsum, DateTime.Parse("2021-01-12"), 2457);
            CreateAndAddNewArticle(2, "Media", "149 days until Elden Ring", SeedTexts.LoremIpsum, DateTime.Parse("2021-08-26"), 99999);
            CreateAndAddNewArticle(4, "Sport", "Standing is not good for you", SeedTexts.LoremIpsum, DateTime.Parse("2021-02-21"), 7864);
            CreateAndAddNewArticle(2, "Media", "Those invaders will drive you crazy...", SeedTexts.LoremIpsum, DateTime.Parse("2020-05-30"), 757);
            CreateAndAddNewArticle(2, "Culture", "That thing! Did you see it?", SeedTexts.LoremIpsum, DateTime.Parse("2020-08-01"), 7567);
            CreateAndAddNewArticle(3, "Culture", "Why the rot is the solution", SeedTexts.LoremIpsum, DateTime.Parse("2021-04-17"), 124);
            _context.SaveChanges();
        }

        private int _nextId;
        private void CreateAndAddNewArticle(long idAuthor, string category, string title, string content, DateTime dateCreated, int viewCount)
        {
            _context.Articles.Add(new Model.Article() { id = _nextId, idAuthor = idAuthor, category = category, title = title, content = content, dateCreated = dateCreated, viewCount = viewCount });
            ++_nextId;
        }
    }
}
