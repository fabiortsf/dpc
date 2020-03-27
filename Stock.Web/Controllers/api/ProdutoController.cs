using Stock.Entity;
using Stock.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Stock.Api.Controllers
{
    public class ProdutoController : ApiController
    {
        [HttpGet, ActionName("ListarTipos")]
        public IHttpActionResult ListarTipos()
        {
            List<ProdutoTipo> listaTipos = new List<ProdutoTipo>();

            EstoqueService estoqueService = new EstoqueService();
            listaTipos = estoqueService.ListarTiposDeProdutos();

            if (listaTipos == null)
            {
                return NotFound();
            }
            return Ok(listaTipos);
        }

        [HttpPost, ActionName("Salvar")]
        public IHttpActionResult Salvar(Produto produto)
        {
            EstoqueService estoqueService = new EstoqueService();

            estoqueService.SalvarProduto(produto);

            return Ok(produto);
        }

    }
}
