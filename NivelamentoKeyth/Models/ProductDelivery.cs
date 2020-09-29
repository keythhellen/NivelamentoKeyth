using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NivelamentoKeyth.Models
{
    [Table("EntregaProduto")]
    public class ProductDelivery
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public Decimal UnitaryValue { get; set; }
        public Decimal DeliveryValue { get; set; }

        public ProductDelivery() { }
    }
}
