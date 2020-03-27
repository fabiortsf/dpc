using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stock.Web.Controllers
{
    public class AppController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult produtoCadastro()
        {
            return View();
        }

        public ActionResult consultaEstoque()
        {
            return View();
        }

        public ActionResult declararPerda()
        {
            return View();
        }

        public ActionResult vendaPedido()
        {
            return View();
        }

        public ActionResult vendaRealizadas()
        {
            return View();
        }

        public ActionResult relatorioItensVendidos()
        {
            return View();
        }

        public ActionResult relatorioItensPerdidos()
        {
            return View();
        }

    }
}