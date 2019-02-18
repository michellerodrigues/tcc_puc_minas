using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DescarteService.Data.JoinFacade;

namespace DescarteService.Data.Models
{
    public class ProdutoDescarte
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        public Guid IdITemEstoque { get; set; }

        [Required]
        public decimal PesoCheio { get; set; }

        [Required]
        public decimal PesoVazio { get; set; }

        [Required]
        public decimal VolumeEmbalagem { get; set; }

        [Required]
        public DateTime DataVecimentoProduto { get; set; }

        [Required]
        public LoteDescarte LoteDescarte { get;set;  }
    }
}
