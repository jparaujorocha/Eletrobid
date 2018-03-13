using Eletrobid.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eletrobid.Models;

namespace Eletrobid.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoDal _produtoDal;
        private readonly IImpostoDal _impostoDal;
        private readonly IImpostoProdutoDal _impostoProdutoDal;
        private readonly ITipoProdutoDal _tipoProduto;
        private readonly IVendaDal _vendaDal;

        public ProdutoController(IProdutoDal produtoDal,IImpostoDal impostoDal, IImpostoProdutoDal impostoProdutoDal, ITipoProdutoDal tipoProduto, IVendaDal vendaDal)
        {
            _produtoDal = produtoDal;
            _impostoDal = impostoDal;
            _impostoProdutoDal = impostoProdutoDal;
            _tipoProduto = tipoProduto;
            _vendaDal = vendaDal;
        }

        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }


        // GET: Produto
        public ActionResult GerenciaProduto()
        {
            var produtos = _produtoDal.ListaProdutos();
            return View(produtos);
        }
    }
}