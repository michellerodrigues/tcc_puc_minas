using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EstoqueService.Data.JoinFacade;

namespace EstoqueService.Data.Models
{
    public class FornecedorProduto: IJoinEntity<Fornecedor>, IJoinEntity<Produto>
    {
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        Fornecedor IJoinEntity<Fornecedor>.Navigation
        {
            get => Fornecedor;
            set => Fornecedor = value;
        }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        Produto IJoinEntity<Produto>.Navigation
        {
            get => Produto;
            set => Produto = value;
        }
    }
}