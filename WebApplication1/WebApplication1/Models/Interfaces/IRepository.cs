using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Interfaces
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Func<T, bool> predicate);

        T GetById(int id);


        void Create(T Entity);
        void Update(T Entity);
        void Delete(T Entity);

        int Count(Func<T, bool> predicate);


        void SaveChanges();

    }
}
