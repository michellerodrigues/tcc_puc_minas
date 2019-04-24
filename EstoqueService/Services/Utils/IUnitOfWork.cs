using System;
using EstoqueService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstoqueService.Services.Util
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int Commit();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
