using EstoqueService.Data.Interfaces;
using EstoqueService.DataContext;
using EstoqueService.Services.Util;
using Microsoft.EntityFrameworkCore;

public class UnitOfWork : IUnitOfWork
{
    public AppDataContext Context { get; }

    public UnitOfWork(AppDataContext context)
    {
        Context = context;
    }
    public void Commit()
    {
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();

    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        throw new System.NotImplementedException();
    }

    int IUnitOfWork.Commit()
    {
        throw new System.NotImplementedException();
    }
}