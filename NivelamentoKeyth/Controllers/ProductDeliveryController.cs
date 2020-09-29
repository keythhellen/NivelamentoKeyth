using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NivelamentoKeyth.Models;

namespace NivelamentoKeyth.Controllers
{
    [ApiController]
    [Route("productdelivery")]
    public class ProductDeliveryController : Controller
    {
        private readonly IProductDeliveryDal _entregaProdutoDal;

        public ProductDeliveryController(IProductDeliveryDal entregaProdutoDal)
        {
            _entregaProdutoDal = entregaProdutoDal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _entregaProdutoDal.GetAllImports();
            return View("Index", result);
        }

        [HttpGet("getallimports")]
        public IActionResult Get()
        {
            var result = _entregaProdutoDal.GetAllImports();
            return View("ImportView", result);
        }

        [HttpPost("import")]
        public IActionResult Import(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("Arquivo não encontrado.");
            }
            else
            {
                //string url = @"C:/TesteAdmissao.xslx";
                _entregaProdutoDal.Validate(file.FileName);
            }
            return RedirectToAction("Get", "ProductDelivery");
        }
    }
}
