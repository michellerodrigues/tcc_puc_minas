using System;
using System.Collections.Generic;
using System.Linq;
using DescarteService.DataContext;
using DescarteService.Data.Interfaces;
using DescarteService.Data.Models;
using DescarteService.Data.Repository;

public class ProdutoDescarteRepository : Repository<ProdutoDescarte>, IProdutoDescarteRepository
{
    public ProdutoDescarteRepository(AppDataContext context) : base(context)
    {
    }
    public IEnumerable<ProdutoDescarte> FindProdutosVencidos()
    {
        return _context.ProdutoDescartes.Where(pd=>pd.DataVecimentoProduto<=DateTime.Now);
    }


}