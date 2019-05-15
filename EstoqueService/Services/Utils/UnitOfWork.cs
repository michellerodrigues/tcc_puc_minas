using EstoqueService.Data.Interfaces;
using EstoqueService.DataContext;
using EstoqueService.Services.Util;
using Microsoft.EntityFrameworkCore;

public class UnitOfWork : IUnitOfWork
{
    //public AppDataContext Context { get; }
    private AppDataContext _context;

    private IEstoqueRepository _estoque;
    public IEstoqueRepository Estoque {get {return _estoque;}}
    
    private IProdutoRepository _produto;
    public IProdutoRepository Produto {get {return _produto;}}

    private IRevendedorRepository _revendedor;
    public IRevendedorRepository Revendedor {get {return _revendedor;}}
    
    private IFabricanteRepository _fabricante;
    public IFabricanteRepository Fabricante {get {return _fabricante;}}


    public UnitOfWork(AppDataContext context)//, IEstoqueRepository estoque)
    {
       
        _context = context;
        _estoque = new EstoqueRepository(_context.Estoques);
        _produto = new ProdutoRepository(_context.Produtos);
        _fabricante = new FabricanteRepository(_context.Fabricantes);
        _revendedor = new RevendedorRepository(_context.Revendedores);
    }
    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();

    }
}