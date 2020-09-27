using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NivelamentoKeyth.Models
{
    public interface IEntregaProdutoDal
    {
        IEnumerable<EntregaProduto> ObterEntregasProdutos();
        int IncluirEntregaProduto(EntregaProduto entregaProduto);
        int AtualizarEntregaProduto(EntregaProduto entregaProduto);
        EntregaProduto ObterEntregaPorId(int id);
        int ExcluirEntregaProduto(int id);
    }
}
