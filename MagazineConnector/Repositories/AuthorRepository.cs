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
        public Model.Author CreateAuthor(Model.Author author)
        {
            _context.Add(author);
            _context.SaveChanges();
            return author;
        }
        public void UpdateAuthor(Model.Author author)
        {
            _context.Update(author);
            _context.SaveChanges();
        }
        public void DeleteAuthor(long id)
        {
            _context.Remove(_context.Authors.First(x => x.id == id));
            _context.SaveChanges();
        }

        public bool AuthorExists(long id)
        {
            return _context.Authors.Any(e => e.id == id);
        }
        public bool AuthorExists(string name)
        {
            return _context.Authors.Any(e => e.name == name);
        }
    }
}
