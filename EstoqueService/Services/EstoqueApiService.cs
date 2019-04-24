using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using EstoqueService.Services.Interfaces;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.DataContext;
using EstoqueService.Services.Util;

namespace EstoqueService.Services.Messages
{
    public class EstoqueApiService: IEstoqueApiService
    {
       private readonly IUnitOfWork _uow;
      
        public EstoqueApiService(IUnitOfWork unit )
        {
            _uow = unit;
        }
    
        public void SomeMethod(SomeClass entity)
        {
            _uow.GetRepository<SomeClass>().Add(entity);
            _uow.Commit();
            
        }

        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos()
        {
            ObterProdutosVencidosMessageResponse response = new ObterProdutosVencidosMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Produtos Vencidos Retornados com sucesso";
            response.LoteProdutosVecidos = new List<ProdutoMessage>();

            var itensEstoqueVencidos = _estoqueRepository.FindItensVencidosEstoque();

            if (itensEstoqueVencidos == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Não existem produtos vencidos no estoque";
            }

            foreach (Estoque estoque in itensEstoqueVencidos)
            {
                var produtoMessage = new ProdutoMessage()
                {
                    DataVencimento = estoque.DataVecimentoProduto.ToShortDateString(),
                    NomeProduto = estoque.Produto.Nome,
                    QtdeprodutoDisponivel = estoque.QtdeDispUnidade.ToString(),
                    IdItemEstoque = estoque.Id,
                    EmailFabricante = estoque.Fabricante.Email,
                    EmailRevendedor = estoque.Revendedor.Email,
                    PesoCheio = estoque.Produto.PesoCheio,
                    PesoVazio = estoque.Produto.PesoVazio,
                    VolumeEmbalagem = estoque.Produto.VolumeEmbalagem
                };
                response.LoteProdutosVecidos.Add(produtoMessage);
            }

            return response;
        }

        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados()
        {
            //_estoqueRepository = new EstoqueRepository(_context);
        
            ObterProdutosFinalizadosMessageResponse response = new ObterProdutosFinalizadosMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Produtos Vazios Retornados com sucesso";
            response.LoteProdutosFinalizados = new List<ProdutoMessage>();

            var itensEstoqueFinalizados = _estoqueRepository.FindItensFinalizadosEstoque().ToList();

            if (itensEstoqueFinalizados == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Não existem produtos finalizados no estoque";
            }

            foreach (Estoque estoque in itensEstoqueFinalizados)
            {
                var produtoMessage = new ProdutoMessage()
                {
                    DataVencimento = estoque.DataVecimentoProduto.ToShortDateString(),
                    NomeProduto = estoque.Produto.Nome,
                    QtdeprodutoDisponivel = estoque.QtdeDispUnidade.ToString(),
                    IdItemEstoque = estoque.Id,
                    EmailFabricante = estoque.Fabricante.Email,
                    EmailRevendedor = estoque.Revendedor.Email,
                    PesoCheio = estoque.Produto.PesoCheio,
                    PesoVazio = estoque.Produto.PesoVazio,
                    VolumeEmbalagem = estoque.Produto.VolumeEmbalagem
                };
                response.LoteProdutosFinalizados.Add(produtoMessage);
            }

            return response;
        }
    }
}