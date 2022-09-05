using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Interfaces
{
    public interface IBookRepository: IRepository<Book>
    {

        IEnumerable<Book> GetAllWithAuthor();
        IEnumerable<Book> FindWithAuthor(Func<Book, bool> predicate);
        IEnumerable<Book> FindWithAuthorAndBorrower(Func<Book, bool> predicate);


    }
}
