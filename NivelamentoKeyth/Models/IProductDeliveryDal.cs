using NivelamentoKeyth.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NivelamentoKeyth.Models
{
    public interface IProductDeliveryDal
    {
        ProductDeliveryViewModel GetAllImports();
        ProductDelivery GetImportById(int id);
        int Insert(List<ProductDelivery> deliveries);
        List<string> Validate(string path);
    }
}
