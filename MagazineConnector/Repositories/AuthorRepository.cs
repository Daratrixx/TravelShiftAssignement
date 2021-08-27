using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Repositories
{
    public class AuthorRepository
    {
        private Contexts.BlogContext _context;
        public AuthorRepository(Contexts.BlogContext context)
        {
            _context = context;
        }
        public IEnumerable<Model.Author> GetAuthors()
        {
            return _context.Authors;
        }
        public Model.Author GetAuthorById(long id)
        {
            return _context.Authors.Where(x => x.id == id).FirstOrDefault();
        }
        public Model.Author GetAuthorByName(string name)
        {
            return _context.Authors.Where(x => x.name == name).FirstOrDefault();
        }
    }
}
