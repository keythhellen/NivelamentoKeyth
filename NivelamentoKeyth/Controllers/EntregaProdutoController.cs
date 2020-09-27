using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NivelamentoKeyth.Models;

namespace NivelamentoKeyth.Controllers
{
    [Route("api/entregaproduto")]
    [ApiController]
    public class EntregaProdutoController : ControllerBase
    {
        private readonly IEntregaProdutoDal _entregaProdutoDal;

        public EntregaProdutoController(IEntregaProdutoDal entregaProdutoDal)
        {
            _entregaProdutoDal = entregaProdutoDal;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _entregaProdutoDal.ObterEntregasProdutos();
            return Ok(result);
        }

        [HttpPost, Route("criarentregaproduto")]
        public IActionResult Create([FromBody] EntregaProduto entregaProduto)
        {
            if (entregaProduto == null)
                return BadRequest();

            var status = _entregaProdutoDal.IncluirEntregaProduto(entregaProduto);

            if (status != 1)
                return BadRequest("Erro ao incluir entrega.");

            return Ok();
        }

        [HttpGet("obterentregaporid/{id}")]
        public IActionResult Get(int id)
        {
            var entregaProduto = _entregaProdutoDal.ObterEntregaPorId(id);

            if (entregaProduto == null)
                return BadRequest("Nenhuma entrega encontrada com esse código.");

            return Ok(entregaProduto);
        }

        [HttpPut, Route("atualizarentregaproduto")]
        public IActionResult Put([FromBody] EntregaProduto entregaProduto)
        {
            if (entregaProduto == null)
                return BadRequest("Nenhuma entrega encontrada com esse código.");

            var status = _entregaProdutoDal.AtualizarEntregaProduto(entregaProduto);

            if (status != 1)
                return BadRequest("Erro ao atualizar entrega");

            return Ok("Entrega atualizada com sucesso.");
        }

        [HttpDelete("excluirentregaproduto/{id}")]
        public IActionResult Delete(int id)
        {
            var status = _entregaProdutoDal.ExcluirEntregaProduto(id);

            if (status != 1)
                return BadRequest("Erro ao excluir entrega.");

            return Ok("Entrega apagada com sucesso.");
        }
    }
}
