using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.Interfaces;
namespace WebApplication1.Models.Implement
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(MyDbContext db) : base(db)
        {
        }

        public IEnumerable<Author> GetAllWithBooks()
        {
            return db.Authors.Include(x => x.Books);
        }

        public Author GetWithBooks(int id)
        {
            return db.Authors.Where(x => x.AuthorId == id).Include(x => x.Books).FirstOrDefault();
        }
    }
}
