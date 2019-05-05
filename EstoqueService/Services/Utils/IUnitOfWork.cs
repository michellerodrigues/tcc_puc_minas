using System;
using EstoqueService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstoqueService.Services.Util
{
    public interface IUnitOfWork : IDisposable
    {
        IEstoqueRepository Estoque { get; }
        IProdutoRepository Produto { get; }
        IFabricanteRepository Fabricante { get; }
        IRevendedorRepository Revendedor { get; }
        void Save();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
