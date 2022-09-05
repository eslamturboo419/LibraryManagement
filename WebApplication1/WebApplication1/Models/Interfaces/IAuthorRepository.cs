using System.Collections.Generic;

namespace WebApplication1.Models.Interfaces
{
    public interface IAuthorRepository: IRepository<Author>
    {

        IEnumerable<Author> GetAllWithBooks();
        Author GetWithBooks(int id);



    }
}
