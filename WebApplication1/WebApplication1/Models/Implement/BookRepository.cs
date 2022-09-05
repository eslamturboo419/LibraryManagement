using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.Interfaces;

namespace WebApplication1.Models.Implement
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MyDbContext db) : base(db)
        {
        }

        public IEnumerable<Book> FindWithAuthor(Func<Book, bool> predicate)
        {
            return db.Books.Include(x => x.Author).Where(predicate);
        }

        public IEnumerable<Book> FindWithAuthorAndBorrower(Func<Book, bool> predicate)
        {
            
            return db.Books.Include(x => x.Author).Include(x=>x.Borrower)
                .Where(predicate);
        }

        public IEnumerable<Book> GetAllWithAuthor()
        {
            return db.Books.Include(x => x.Author);
        }
    }
}
