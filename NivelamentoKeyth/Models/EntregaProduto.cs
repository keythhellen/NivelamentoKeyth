using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NivelamentoKeyth.Models
{
    [Table("EntregaProduto")]
    public class EntregaProduto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DataEntrega { get; set; }
        [Required]
        public string NomeProduto { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public Decimal ValorUnitario { get; set; }

        public EntregaProduto() { }
    }
}
