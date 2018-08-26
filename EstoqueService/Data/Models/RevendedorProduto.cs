using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EstoqueService.Data.JoinFacade;

namespace EstoqueService.Data.Models
{
    public class RevendedorProduto: IJoinEntity<Revendedor>, IJoinEntity<Produto>
    {
        public int RevendedorId { get; set; }
        public Revendedor Revendedor { get; set; }

        Revendedor IJoinEntity<Revendedor>.Navigation
        {
            get => Revendedor;
            set => Revendedor = value;
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
