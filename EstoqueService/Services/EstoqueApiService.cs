using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using EstoqueService.Data.Interfaces;
using EstoqueService.Data.Models;
using EstoqueService.DataContext;

namespace EstoqueService.Services.Messages
{
    public class EstoqueApiService
    {

        public ObterProdutosVencidosMessageResponse ObterProdutosVencidos(AppDataContext _context)
        {
            IEstoqueRepository estoqueRepository = new EstoqueRepository(_context);

            ObterProdutosVencidosMessageResponse response = new ObterProdutosVencidosMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Produtos Vencidos Retornados com sucesso";
            response.LoteProdutosVecidos = new List<ProdutoMessage>();

            var itensEstoqueVencidos = estoqueRepository.FindItensVencidosEstoque();

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
                    EmailFornecedor = estoque.Fornecedor.Email,
                    EmailRevendedor = estoque.Revendedor.Email,
                    PesoCheio = estoque.Produto.PesoCheio,
                    PesoVazio = estoque.Produto.PesoVazio,
                    VolumeEmbalagem = estoque.Produto.VolumeEmbalagem
                };
                response.LoteProdutosVecidos.Append(produtoMessage);
            }

            return response;
        }

        public ObterProdutosFinalizadosMessageResponse ObterProdutosFinalizados(AppDataContext _context)
        {
            IEstoqueRepository estoqueRepository = new EstoqueRepository(_context);
        
            ObterProdutosFinalizadosMessageResponse response = new ObterProdutosFinalizadosMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Produtos Vazios Retornados com sucesso";
            response.LoteProdutosFinalizados = new List<ProdutoMessage>();

            var itensEstoqueFinalizados = estoqueRepository.FindItensFinalizadosEstoque();

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
                    EmailFornecedor = estoque.Fornecedor.Email,
                    EmailRevendedor = estoque.Revendedor.Email,
                    PesoCheio = estoque.Produto.PesoCheio,
                    PesoVazio = estoque.Produto.PesoVazio,
                    VolumeEmbalagem = estoque.Produto.VolumeEmbalagem
                };
                response.LoteProdutosFinalizados.Append(produtoMessage);
            }

            return response;
        }
    }
}