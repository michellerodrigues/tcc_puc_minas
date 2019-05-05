using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DescarteService.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Delete(T entity);
        void Update(T entity);
        int Count(Func<T, bool> predicate);
        void Create(T entity);
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> GetAll();        
        T GetById(Guid id);

      }
}