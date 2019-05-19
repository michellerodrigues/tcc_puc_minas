using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DescarteService.Data.JoinFacade;

namespace DescarteService.Data.Models
{
    public class LoteDescarte
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NomeResponsavelDescarte { get; set; }

        [Required]
        [StringLength(255)]
        public string EmailResponsavelDescarte { get; set; }
        public ICollection<ProdutoDescarte> ProdutosDescartes { get;} = new List<ProdutoDescarte>();
    }
}
