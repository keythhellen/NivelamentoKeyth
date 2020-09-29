using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NivelamentoKeyth.Models.ViewModels
{
    [Table("EntregaProduto")]
    public class ProductDeliveryViewModel
    {
        public DateTime OldestDelivery { get; set; }
        public List<ProductDelivery> ProductDeliveryList { get; }

        public ProductDeliveryViewModel() {
            ProductDeliveryList = new List<ProductDelivery>();
        }

        public void SetProductDeliveryList(List<ProductDelivery> productDeliveryList)
        {
            ProductDeliveryList.AddRange(productDeliveryList);
        }
    }
}
