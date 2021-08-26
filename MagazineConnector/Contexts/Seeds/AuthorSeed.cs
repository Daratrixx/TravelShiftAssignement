using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Contexts.Seeds
{
    public class AuthorSeed
    {
        private readonly BlogContext _context;

        public AuthorSeed(BlogContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            _nextId = 1;
            CreateAndAddNewAuthor("John Doe");
            CreateAndAddNewAuthor("Solaire of Astora");
            CreateAndAddNewAuthor("Five Pebbles");
            CreateAndAddNewAuthor("THE MAN");
            _context.SaveChanges();
        }

        private int _nextId;
        private void CreateAndAddNewAuthor(string name)
        {
            _context.Authors.Add(new Model.Author() { id = _nextId, name = name });
            ++_nextId;
        }
    }
}
