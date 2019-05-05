using System;
using System.Collections.Generic;
using System.Linq;
using EstoqueService.DataContext;
using EstoqueService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using EstoqueService.Services.Util;

namespace EstoqueService.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {

        protected DbSet<T> _dbSet;
        public Repository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}