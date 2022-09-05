using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.Interfaces;
namespace WebApplication1.Models.Implement
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected  MyDbContext db;

        public Repository(MyDbContext db)
        {
            this.db = db;
        }

        public int Count(Func<T, bool> predicate)
        {
           return db.Set<T>().Where(predicate).Count();
        }


        public void Create(T Entity)
        {
            db.Add(Entity);
            SaveChanges();
        }

        public void Delete(T Entity)
        {
            db.Remove(Entity);
            db.SaveChanges();
        }

        /// this is delages which pass 
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            var val = db.Set<T>().Where(predicate);
            return val;
        }

        public IEnumerable<T> GetAll()
        {
           return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            var val = db.Set<T>().Find(id);
            return val;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Update(T Entity)
        {
            db.Entry(Entity).State = EntityState.Modified;
            SaveChanges();
        }
    }
}
