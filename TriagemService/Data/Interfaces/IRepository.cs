using System;
using System.Collections.Generic;

namespace TriagemService.Data.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Func<T, bool> predicate);

        T GetById(int id);

        T GetById(Guid id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        int Count(Func<T, bool> predicate);


    }
}